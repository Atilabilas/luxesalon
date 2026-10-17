using System.Xml.Linq;
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

public class BlogEditorialTests
{
    private static readonly string[] Slugs =
    {
        "artisan-balayage-nedir-klasik-balyajdan-farki",
        "sac-rengi-duzeltme-gercekci-beklenti",
        "saca-protein-bakimi-mi-nem-bakimi-mi",
        "kas-laminasyonu-nedir-kimlere-uygun"
    };

    [Fact]
    public async Task Seed_PublishesThreeBackdatedTurkishArticlesAndKeepsBrowsDraft()
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();
        using var db = CreateDb(connection);
        DataSeeder.Seed(db);

        var posts = db.BlogPosts.Where(p => Slugs.Contains(p.Slug)).OrderBy(p => p.PublishedAt).ToList();
        Assert.Equal(4, posts.Count);
        Assert.Equal(new[] { "2026-09-30", "2026-10-07", "2026-10-14", "2026-10-18" },
            posts.Select(p => p.PublishedAt.ToString("yyyy-MM-dd")));
        Assert.Equal(new[] { "Celil", "Celil", "Celil", "Celil & Atilla" }, posts.Select(p => p.Author));
        Assert.All(posts.Take(3), p => Assert.True(p.IsPublished));
        Assert.False(posts[3].IsPublished);
        Assert.All(posts, p => Assert.False(p.HasEnglish));

        using var cache = new MemoryCache(new MemoryCacheOptions());
        var page = new SitemapModel(db, new SiteSettingsService(db, cache))
        {
            PageContext = new PageContext { HttpContext = new DefaultHttpContext() }
        };
        await page.OnGetAsync();
        XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        XNamespace xhtml = "http://www.w3.org/1999/xhtml";
        var urls = XDocument.Parse(page.Xml).Descendants(ns + "url").ToList();
        foreach (var slug in Slugs)
        {
            if (slug == Slugs[3])
            {
                Assert.DoesNotContain(urls, u => u.Element(ns + "loc")!.Value == "https://celilatilla.com/blog/" + slug);
                continue;
            }
            var url = Assert.Single(urls, u => u.Element(ns + "loc")!.Value == "https://celilatilla.com/blog/" + slug);
            Assert.DoesNotContain(url.Elements(xhtml + "link"), link => link.Attribute("hreflang")?.Value == "en");
            Assert.DoesNotContain(urls, u => u.Element(ns + "loc")!.Value == "https://celilatilla.com/en/blog/" + slug);
        }
    }

    [Fact]
    public void Seed_UpdatesFringeSectionsWithoutChangingItsAddress()
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();
        using var db = CreateDb(connection);
        DataSeeder.Seed(db);

        var post = db.BlogPosts.Single(p => p.Slug == "kahkul-kesimi-yuz-sekli-sac-yapisi-ve-bakim");
        Assert.Equal("Kahkül Kesimi: Yüzüne Göre Düz, Perde ve Yan Kahkül Modelleri", post.Title);
        Assert.Contains("<h2>Düz kahkül kesimi kimlere yakışır?</h2>", post.Content);
        Assert.Contains("<h2>Yan kahkül nasıl şekillendirilir?</h2>", post.Content);
        Assert.Contains("<h2>Kahkül nasıl kesilir, evde kesmek neden riskli?</h2>", post.Content);
        Assert.Contains("duz-kahkul.webp", post.Content);
        Assert.Contains("yan-kahkul.webp", post.Content);
        Assert.Equal(new DateTime(2026, 10, 18), post.UpdatedAt);
        Assert.Equal("Bangs: Face Shape, Hair Type and Upkeep", post.TitleEn);
    }

    [Fact]
    public void Apply_PreservesExistingDatesAndOtherContentAndRunsOnlyOnce()
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();
        using var db = CreateDb(connection);
        DataSeeder.Seed(db);
        db.BlogPosts.RemoveRange(db.BlogPosts.Where(p => Slugs.Contains(p.Slug)));
        var fringe = db.BlogPosts.Single(p => p.Slug == BlogEditorialContent.FringeSlug);
        var id = fringe.Id;
        fringe.PublishedAt = new DateTime(2025, 9, 1);
        fringe.Content = "<h2>Perde kahkül kimlere uygundur?</h2><p>Mevcut perde metni.</p>" +
            "<h2>Evde kahkül kesmek neden riskli?</h2><p>Mevcut risk metni.</p>";
        var other = db.BlogPosts.First(p => p.Id != id);
        other.Title = "CMS başlığı";
        other.Content = "Korunacak CMS içeriği";
        other.IsPublished = false;
        db.SaveChanges();

        BlogEditorialContent.Apply(db);

        Assert.Equal(id, fringe.Id);
        Assert.Equal(new DateTime(2025, 9, 1), fringe.PublishedAt);
        Assert.Contains("Mevcut perde metni.", fringe.Content);
        Assert.Contains("Mevcut risk metni.", fringe.Content);
        Assert.Equal("CMS başlığı", other.Title);
        Assert.Equal("Korunacak CMS içeriği", other.Content);
        Assert.False(other.IsPublished);
        Assert.Equal(4, db.BlogPosts.Count(p => Slugs.Contains(p.Slug)));
        var article = db.BlogPosts.Single(p => p.Slug == Slugs[0]);
        article.Content = "Aktarım sonrası CMS düzenlemesi";
        article.IsPublished = false;
        fringe.Content = "Güncelleme sonrası kahkül içeriği";
        db.SaveChanges();

        BlogEditorialContent.Apply(db);
        db.ChangeTracker.Clear();

        Assert.Equal("Aktarım sonrası CMS düzenlemesi", db.BlogPosts.Single(p => p.Id == article.Id).Content);
        Assert.False(db.BlogPosts.Single(p => p.Id == article.Id).IsPublished);
        Assert.Equal("Güncelleme sonrası kahkül içeriği", db.BlogPosts.Single(p => p.Id == id).Content);
        Assert.Single(db.SiteSettings.Where(s => s.Key == BlogEditorialContent.RevisionKey));
    }

    private static ApplicationDbContext CreateDb(SqliteConnection connection)
    {
        var db = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(connection).Options);
        db.Database.EnsureCreated();
        return db;
    }
}
