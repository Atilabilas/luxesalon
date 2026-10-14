using System.Xml.Linq;
using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Web.Pages;
using LuxeSalon.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Xunit;

namespace LuxeSalon.Tests;

public class SitemapTests
{
    [Fact]
    public async Task Sitemap_ExcludesWhitespaceSlugs_AndUnpublishedOrNonLandingPages()
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();
        using var db = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(connection).Options);
        db.Database.EnsureCreated();
        db.Services.AddRange(
            new Service { Name = "Boş adres", Slug = "   ", IsPublished = true, IsLandingPage = true },
            new Service { Name = "Taslak", Slug = "taslak-hizmet", IsPublished = false, IsLandingPage = true },
            new Service { Name = "Kart", Slug = "hizmet-karti", IsPublished = true, IsLandingPage = false },
            new Service { Name = "Kesim", Slug = "kesim", IsPublished = true, IsLandingPage = true });
        db.BlogPosts.AddRange(
            new BlogPost { Title = "Boş adres", Slug = "   ", Content = "İçerik", IsPublished = true },
            new BlogPost { Title = "Taslak", Slug = "taslak-yazi", Content = "İçerik", IsPublished = false });
        db.SaveChanges();
        using var cache = new MemoryCache(new MemoryCacheOptions());
        var page = new SitemapModel(db, new SiteSettingsService(db, cache)) { PageContext = new PageContext { HttpContext = new DefaultHttpContext() } };

        await page.OnGetAsync();

        XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        var urls = XDocument.Parse(page.Xml).Descendants(ns + "loc").Select(e => e.Value).ToList();
        Assert.Contains("https://celilatilla.com/hizmetler/kesim", urls);
        Assert.Contains("https://celilatilla.com/izmir-ilceleri", urls);
        Assert.Contains("https://celilatilla.com/en/izmir-districts", urls);
        Assert.DoesNotContain(urls, url => url.EndsWith("   ") || url.Contains("taslak-") || url.EndsWith("/hizmet-karti"));
    }

    [Fact]
    public async Task Sitemap_ReflectsLocalPageUpdates_AndPreservesNewerServiceDates()
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();
        using var db = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(connection).Options);
        db.Database.EnsureCreated();
        db.Services.AddRange(
            new Service { Name = "Kesim", Slug = "kadin-sac-kesimi", IsPublished = true, IsLandingPage = true, UpdatedAt = new DateTime(2026, 10, 5) },
            new Service { Name = "Gelin saçı", Slug = "gelin-saci", SlugEn = "bridal-hair", NameEn = "Bridal hair", LongDescriptionEn = "Bridal styling", IsPublished = true, IsLandingPage = true, UpdatedAt = new DateTime(2026, 10, 15) });
        db.BlogPosts.AddRange(
            new BlogPost { Title = "Kesim rehberi", Slug = "kesim-rehberi", Content = "Kesim bilgileri", TitleEn = "Cutting guide", SlugEn = "cutting-guide", ContentEn = "Cutting advice", IsPublished = true, PublishedAt = new DateTime(2026, 9, 1), UpdatedAt = new DateTime(2026, 10, 5) },
            new BlogPost { Title = "Renk rehberi", Slug = "renk-rehberi", Content = "Renk bilgileri", IsPublished = true, PublishedAt = new DateTime(2026, 9, 1), UpdatedAt = new DateTime(2026, 10, 15) });
        db.SaveChanges();
        using var cache = new MemoryCache(new MemoryCacheOptions());
        var page = new SitemapModel(db, new SiteSettingsService(db, cache)) { PageContext = new PageContext { HttpContext = new DefaultHttpContext() } };

        await page.OnGetAsync();

        XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        var entries = XDocument.Parse(page.Xml).Descendants(ns + "url").ToDictionary(e => e.Element(ns + "loc")!.Value);
        foreach (var path in new[] { "/", "/en", "/hizmetler/kadin-sac-kesimi", "/blog/kesim-rehberi", "/en/blog/cutting-guide" })
            Assert.Equal("2026-10-11", entries["https://celilatilla.com" + path].Element(ns + "lastmod")?.Value);
        foreach (var path in new[] { "/hizmetler", "/en/services", "/hizmetler/gelin-saci", "/en/services/bridal-hair", "/blog", "/en/blog", "/blog/renk-rehberi" })
            Assert.Equal("2026-10-15", entries["https://celilatilla.com" + path].Element(ns + "lastmod")?.Value);
    }

    [Fact]
    public async Task Sitemap_EscapesUrls_AndExcludesUntranslatedServices()
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();
        using var db = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(connection).Options);
        db.Database.EnsureCreated();
        db.Services.Add(new Service { Name = "Kesim", Slug = "kesim", SlugEn = "cut", LongDescriptionEn = "English description", NameEn = "", IsPublished = true, IsLandingPage = true });
        db.BlogPosts.Add(new BlogPost { Title = "Renk", Slug = "renk&bakim", Content = "İçerik", IsPublished = true });
        db.SaveChanges();
        using var cache = new MemoryCache(new MemoryCacheOptions());
        var page = new SitemapModel(db, new SiteSettingsService(db, cache)) { PageContext = new PageContext { HttpContext = new DefaultHttpContext() } };

        await page.OnGetAsync();

        var xml = XDocument.Parse(page.Xml);
        XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        var urls = xml.Descendants(ns + "loc").Select(e => e.Value).ToList();
        Assert.Contains("https://celilatilla.com/blog/renk&bakim", urls);
        Assert.DoesNotContain("https://celilatilla.com/en/services/cut", urls);
    }
}
