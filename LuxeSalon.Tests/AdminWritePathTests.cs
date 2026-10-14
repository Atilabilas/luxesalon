using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using BlogCreateModel = LuxeSalon.Web.Pages.Admin.Blog.CreateModel;
using BlogEditModel = LuxeSalon.Web.Pages.Admin.Blog.EditModel;
using ServiceEditModel = LuxeSalon.Web.Pages.Admin.Services.EditModel;
using SettingsIndexModel = LuxeSalon.Web.Pages.Admin.Settings.IndexModel;
using LuxeSalon.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using Xunit;

namespace LuxeSalon.Tests;

public class AdminWritePathTests
{
    [Fact]
    public async Task ServiceEdit_UpdatesPriceWithoutErasingPublishedAddress()
    {
        await using var db = await CreateDb();
        db.Services.Add(new Service
        {
            Name = "Balyaj",
            NameEn = "Balayage",
            Slug = "balyaj",
            SlugEn = "balayage",
            Description = "eski",
            DescriptionEn = "old",
            LongDescription = "uzun",
            LongDescriptionEn = "long",
            SeoTitle = "Balyaj SEO",
            MetaDescription = "meta",
            Tags = "renk",
            Price = 100,
            DurationMinutes = 90,
            Category = "Saç",
            ImageUrl = "/images/services/balyaj.jpg",
            IsLandingPage = true,
            IsPublished = false,
            UpdatedAt = new DateTime(2020, 1, 1)
        });
        await db.SaveChangesAsync();
        db.ChangeTracker.Clear();

        var page = new ServiceEditModel(db, new FakeEnv())
        {
            Service = new Service
            {
                Id = 1,
                Name = "Balyaj",
                Description = "yeni fiyat metni",
                Price = 150,
                DurationMinutes = 75,
                Category = "Saç",
                ImageUrl = "/images/services/balyaj.jpg"
            }
        };

        var result = await page.OnPostAsync(null);

        var saved = await db.Services.SingleAsync();
        Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal(150, saved.Price);
        Assert.Equal("yeni fiyat metni", saved.Description);
        Assert.Equal("balyaj", saved.Slug);
        Assert.Equal("balayage", saved.SlugEn);
        Assert.Equal("Balayage", saved.NameEn);
        Assert.Equal("uzun", saved.LongDescription);
        Assert.Equal("long", saved.LongDescriptionEn);
        Assert.Equal("Balyaj SEO", saved.SeoTitle);
        Assert.Equal("meta", saved.MetaDescription);
        Assert.Equal("renk", saved.Tags);
        Assert.True(saved.IsLandingPage);
        Assert.False(saved.IsPublished);
        Assert.True(saved.UpdatedAt > new DateTime(2020, 1, 1));
    }

    [Fact]
    public async Task BlogCreate_PublishesUniqueSlugAndRefusesEmptyContent()
    {
        await using var db = await CreateDb();
        db.BlogPosts.Add(new BlogPost { Title = "Saç Botoksu", Slug = "sac-botoksu", Content = "Eski", IsPublished = true });
        await db.SaveChangesAsync();
        var page = new BlogCreateModel(db, new FakeEnv()) { PageContext = new PageContext { HttpContext = new DefaultHttpContext() } };

        page.OnGet();
        Assert.Equal("Celil & Atilla", page.BlogPost.Author);

        page.BlogPost = new BlogPost { Title = "Saç Botoksu", Content = "Yeni metin", Summary = "özet", Author = "" };
        var created = await page.OnPostAsync(null);
        var post = await db.BlogPosts.OrderBy(p => p.Id).LastAsync();
        Assert.IsType<RedirectToPageResult>(created);
        Assert.Equal("sac-botoksu-2", post.Slug);
        Assert.Equal("Celil & Atilla", post.Author);

        page.BlogPost = new BlogPost { Title = "Boş", Content = "   ", Author = "Celil & Atilla" };
        var rejected = await page.OnPostAsync(null);
        Assert.IsType<PageResult>(rejected);
        Assert.Equal(2, await db.BlogPosts.CountAsync());
    }

    [Fact]
    public async Task BlogEdit_StampsUpdatedAtOnlyWhenContentChanges()
    {
        await using var db = await CreateDb();
        var original = new DateTime(2026, 1, 1);
        db.BlogPosts.Add(new BlogPost
        {
            Title = "Eski başlık",
            Slug = "eski-baslik",
            Content = "Eski içerik",
            Summary = "özet",
            Author = "Celil & Atilla",
            HeaderImageUrl = "/uploads/blog/kapak.jpg",
            IsPublished = true,
            PublishedAt = original,
            UpdatedAt = original
        });
        await db.SaveChangesAsync();
        var page = new BlogEditModel(db, new FakeEnv());

        page.BlogPost = new BlogPost
        {
            Id = 1,
            Title = "Eski başlık",
            Content = "Eski içerik",
            Summary = "özet",
            Author = "Atölye notu",
            HeaderImageUrl = "/uploads/blog/kapak.jpg",
            IsPublished = true
        };
        await page.OnPostAsync(null);
        var afterAuthor = await db.BlogPosts.SingleAsync();
        Assert.Equal(original, afterAuthor.UpdatedAt);
        Assert.Equal("Atölye notu", afterAuthor.Author);

        page.BlogPost.Content = "Güncel içerik";
        page.BlogPost.Author = "Atölye notu";
        await page.OnPostAsync(null);
        var afterContent = await db.BlogPosts.SingleAsync();
        Assert.Equal("Güncel içerik", afterContent.Content);
        Assert.True(afterContent.UpdatedAt > original);
    }

    [Fact]
    public async Task Settings_RejectInvalidCoordinatesAndKeepTheStoredPair()
    {
        await using var db = await CreateDb();
        db.SiteSettings.AddRange(
            new SiteSetting { Key = "Map_Lat", Value = "38.46234930909974", Group = "Map" },
            new SiteSetting { Key = "Map_Lng", Value = "27.208800699999998", Group = "Map" });
        await db.SaveChangesAsync();
        using var cache = new MemoryCache(new MemoryCacheOptions());
        var http = new DefaultHttpContext();
        var page = new SettingsIndexModel(db, new SiteSettingsService(db, cache))
        {
            PageContext = new PageContext { HttpContext = http },
            TempData = new TempDataDictionary(http, new EmptyTempData()),
            FormValues = new Dictionary<string, string>
            {
                ["Map_Lat"] = "38,46234930909974",
                ["Map_Lng"] = "27.208800699999998"
            }
        };

        var result = await page.OnPostAsync();

        Assert.IsType<PageResult>(result);
        Assert.False(page.ModelState.IsValid);
        Assert.Equal("38.46234930909974", (await db.SiteSettings.SingleAsync(s => s.Key == "Map_Lat")).Value);
    }

    private static async Task<ApplicationDbContext> CreateDb()
    {
        var connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync();
        var db = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(connection).Options);
        await db.Database.EnsureCreatedAsync();
        return db;
    }

    private sealed class EmptyTempData : ITempDataProvider
    {
        public IDictionary<string, object> LoadTempData(HttpContext context) => new Dictionary<string, object>();
        public void SaveTempData(HttpContext context, IDictionary<string, object> values) { }
    }

    private sealed class FakeEnv : IWebHostEnvironment
    {
        public string ApplicationName { get; set; } = "LuxeSalon.Tests";
        public IFileProvider WebRootFileProvider { get; set; } = new NullFileProvider();
        public string WebRootPath { get; set; } = Path.GetTempPath();
        public string EnvironmentName { get; set; } = "Development";
        public string ContentRootPath { get; set; } = Path.GetTempPath();
        public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
    }
}
