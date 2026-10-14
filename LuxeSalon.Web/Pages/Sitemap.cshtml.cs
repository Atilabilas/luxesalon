using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security;

namespace LuxeSalon.Web.Pages;

public class SitemapModel : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly SiteSettingsService _settings;

    public SitemapModel(ApplicationDbContext db, SiteSettingsService settings)
    {
        _db = db;
        _settings = settings;
    }

    public string Xml { get; private set; } = "";

    public async Task<IActionResult> OnGetAsync()
    {
        var settings = await _settings.GetSettingsAsync();
        var root = settings.GetValueOrDefault("Site_Url", "https://celilatilla.com").TrimEnd('/');

        var posts = await _db.BlogPosts
            .Where(b => b.IsPublished && b.Slug != null && b.Slug != "")
            .Select(b => new { b.Slug, b.SlugEn, b.TitleEn, b.ContentEn, b.UpdatedAt, b.PublishedAt })
            .ToListAsync();

        var services = await _db.Services
            .Where(s => s.IsPublished && s.IsLandingPage && s.Slug != null && s.Slug != "")
            .Select(s => new { s.Slug, s.SlugEn, s.NameEn, s.LongDescriptionEn, s.UpdatedAt })
            .ToListAsync();

        // Detail handlers reject whitespace-only slugs; the sitemap must match them.
        posts.RemoveAll(p => string.IsNullOrWhiteSpace(p.Slug));
        services.RemoveAll(s => string.IsNullOrWhiteSpace(s.Slug));

        // Significant home/service template changes, not the sitemap request date.
        var localContentUpdatedAt = LocalSalonCopy.UpdatedAt;
        var servicesUpdatedAt = services.Select(s => s.UpdatedAt).Append(localContentUpdatedAt).Max();
        var blogUpdatedAt = posts.Select(p => p.UpdatedAt > p.PublishedAt ? p.UpdatedAt : p.PublishedAt).Append(localContentUpdatedAt).Max();

        var sb = new StringBuilder();
        sb.AppendLine("""<?xml version="1.0" encoding="UTF-8"?>""");
        sb.AppendLine("""<urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9" xmlns:xhtml="http://www.w3.org/1999/xhtml">""");

        void WriteUrl(string loc, string? altTr, string? altEn, DateTime? lastmod = null)
        {
            sb.AppendLine("  <url>");
            sb.AppendLine($"    <loc>{SecurityElement.Escape(root + loc)}</loc>");
            if (lastmod.HasValue && lastmod.Value.Year > 1)
                sb.AppendLine($"    <lastmod>{lastmod.Value:yyyy-MM-dd}</lastmod>");
            if (!string.IsNullOrWhiteSpace(altTr))
                sb.AppendLine($"""    <xhtml:link rel="alternate" hreflang="tr" href="{SecurityElement.Escape(root + altTr)}" />""");
            if (!string.IsNullOrWhiteSpace(altEn))
                sb.AppendLine($"""    <xhtml:link rel="alternate" hreflang="en" href="{SecurityElement.Escape(root + altEn)}" />""");
            if (!string.IsNullOrWhiteSpace(altTr))
                sb.AppendLine($"""    <xhtml:link rel="alternate" hreflang="x-default" href="{SecurityElement.Escape(root + altTr)}" />""");
            sb.AppendLine("  </url>");
        }

        WriteUrl("/", "/", "/en", localContentUpdatedAt);
        WriteUrl("/en", "/", "/en", localContentUpdatedAt);
        WriteUrl("/hakkimizda", "/hakkimizda", "/en/about");
        WriteUrl("/en/about", "/hakkimizda", "/en/about");
        WriteUrl("/hizmetler", "/hizmetler", "/en/services", servicesUpdatedAt);
        WriteUrl("/en/services", "/hizmetler", "/en/services", servicesUpdatedAt);
        WriteUrl("/blog", "/blog", "/en/blog", blogUpdatedAt);
        WriteUrl("/en/blog", "/blog", "/en/blog", blogUpdatedAt);
        WriteUrl("/randevu", "/randevu", "/en/appointment");
        WriteUrl("/en/appointment", "/randevu", "/en/appointment");
        WriteUrl("/gizlilik", "/gizlilik", "/en/privacy");
        WriteUrl("/en/privacy", "/gizlilik", "/en/privacy");
        WriteUrl("/izmir-ilceleri", "/izmir-ilceleri", "/en/izmir-districts", new DateTime(2026, 10, 14));
        WriteUrl("/en/izmir-districts", "/izmir-ilceleri", "/en/izmir-districts", new DateTime(2026, 10, 14));

        foreach (var s in services)
        {
            var tr = $"/hizmetler/{s.Slug}";
            var en = !string.IsNullOrWhiteSpace(s.SlugEn) && !string.IsNullOrWhiteSpace(s.NameEn) && !string.IsNullOrWhiteSpace(s.LongDescriptionEn)
                ? $"/en/services/{s.SlugEn}"
                : null;
            var last = s.UpdatedAt > localContentUpdatedAt ? s.UpdatedAt : localContentUpdatedAt;
            WriteUrl(tr, tr, en, last);
            if (en != null)
                WriteUrl(en, tr, en, last);
        }

        foreach (var p in posts)
        {
            var tr = $"/blog/{p.Slug}";
            var en = !string.IsNullOrWhiteSpace(p.SlugEn) && !string.IsNullOrWhiteSpace(p.TitleEn) && !string.IsNullOrWhiteSpace(p.ContentEn)
                ? $"/en/blog/{p.SlugEn}"
                : null;
            var last = new[] { p.UpdatedAt, p.PublishedAt, localContentUpdatedAt }.Max();
            WriteUrl(tr, tr, en, last);
            if (en != null)
                WriteUrl(en, tr, en, last);
        }

        sb.AppendLine("</urlset>");
        Xml = sb.ToString();
        Response.ContentType = "application/xml; charset=utf-8";
        return Page();
    }
}
