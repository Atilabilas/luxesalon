using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Web.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LuxeSalon.Tests;

public class CanonicalRedirectTests
{
    [Theory]
    [InlineData("http", "www.celilatilla.com", "/Blog/Details/7/", "https://celilatilla.com/blog/renk-rehberi")]
    [InlineData("http", "celilatilla.com", "/EN/Blog/COLOUR-GUIDE/", "https://celilatilla.com/en/blog/colour-guide")]
    [InlineData("https", "celilatilla.com", "/en/blog/renk-rehberi/", "https://celilatilla.com/en/blog/colour-guide")]
    [InlineData("https", "celilatilla.com", "/Hizmetler/3/", "https://celilatilla.com/hizmetler/balyaj")]
    [InlineData("https", "celilatilla.com", "/Blog/Index/", "https://celilatilla.com/blog")]
    [InlineData("https", "celilatilla.com", "/en/", "https://celilatilla.com/en")]
    public async Task Variants_RedirectDirectlyToFinalUrl(string scheme, string host, string path, string target)
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();
        using var db = CreateDb(connection);
        var http = Request(scheme, host, path);
        http.Request.QueryString = new QueryString("?utm_source=test");
        var middleware = new CanonicalRedirectMiddleware(_ => Task.CompletedTask);

        await middleware.InvokeAsync(http, db);

        Assert.Equal(StatusCodes.Status301MovedPermanently, http.Response.StatusCode);
        Assert.Equal(target + "?utm_source=test", http.Response.Headers.Location.ToString());
    }

    [Fact]
    public async Task UnpublishedNumericPost_IsNotRedirectedToPublicContent()
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();
        using var db = CreateDb(connection);
        db.BlogPosts.Single().IsPublished = false;
        db.SaveChanges();
        var http = Request("https", "celilatilla.com", "/Blog/Details/7");

        await new CanonicalRedirectMiddleware(_ => Task.CompletedTask).InvokeAsync(http, db);

        Assert.Equal(StatusCodes.Status404NotFound, http.Response.StatusCode);
        Assert.False(http.Response.Headers.ContainsKey("Location"));
    }

    [Fact]
    public async Task CanonicalPage_AndPostRequests_ArePassedThrough()
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();
        using var db = CreateDb(connection);
        var calls = 0;
        var middleware = new CanonicalRedirectMiddleware(_ => { calls++; return Task.CompletedTask; });
        await middleware.InvokeAsync(Request("https", "celilatilla.com", "/blog/renk-rehberi"), db);
        var post = Request("http", "localhost:5100", "/Randevu");
        post.Request.Method = "POST";
        await middleware.InvokeAsync(post, db);

        Assert.Equal(2, calls);
    }

    [Theory]
    [InlineData("/blog/999999999999999999999")]
    [InlineData("/hizmetler/999999999999999999999")]
    public async Task InvalidNumericId_Returns404(string path)
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();
        using var db = CreateDb(connection);
        var http = Request("https", "celilatilla.com", path);

        await new CanonicalRedirectMiddleware(_ => Task.CompletedTask).InvokeAsync(http, db);

        Assert.Equal(StatusCodes.Status404NotFound, http.Response.StatusCode);
    }

    private static DefaultHttpContext Request(string scheme, string host, string path)
    {
        var http = new DefaultHttpContext();
        http.Request.Method = "GET";
        http.Request.Scheme = scheme;
        http.Request.Host = new HostString(host);
        http.Request.Path = path;
        return http;
    }

    private static ApplicationDbContext CreateDb(SqliteConnection connection)
    {
        var db = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(connection).Options);
        db.Database.EnsureCreated();
        db.BlogPosts.Add(new BlogPost { Id = 7, Title = "Renk rehberi", Slug = "renk-rehberi", TitleEn = "Colour guide", SlugEn = "colour-guide", Content = "İçerik", ContentEn = "Content", IsPublished = true });
        db.Services.Add(new Service { Id = 3, Name = "Balyaj", Slug = "balyaj", IsPublished = true, IsLandingPage = true });
        db.SaveChanges();
        return db;
    }
}
