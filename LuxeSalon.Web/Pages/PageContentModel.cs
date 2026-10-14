using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Web.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LuxeSalon.Web.Pages;

public abstract class PageContentModel : PageModel
{
    protected readonly ApplicationDbContext Context;
    protected readonly ContentService Cms;
    protected readonly SiteSettingsService Settings;

    protected PageContentModel(ApplicationDbContext context, ContentService cms, SiteSettingsService settings)
    {
        Context = context;
        Cms = cms;
        Settings = settings;
    }

    public Dictionary<string, SiteContent> Contents { get; set; } = new();
    public Dictionary<string, string> SiteSettings { get; set; } = new();
    public string CurrentCulture { get; private set; } = "tr";
    public bool IsEnglish => CurrentCulture == "en";

    public string GetContent(string key, string fallback = "") =>
        Cms.GetContent(Contents, key, fallback);

    public int? GetContentId(string key) =>
        Cms.GetContentId(Contents, key);

    public string Setting(string key, string fallback = "") =>
        SiteSettings.GetValueOrDefault(key, fallback);

    public string SiteUrl => Setting("Site_Url", "https://celilatilla.com").TrimEnd('/');

    public void SetSeo(
        string title,
        string? description = null,
        string? canonicalPath = null,
        string? ogImage = null,
        string ogType = "website",
        string? robots = null,
        string? hreflangTr = null,
        string? hreflangEn = null)
    {
        ViewData["Title"] = title;
        if (!string.IsNullOrWhiteSpace(description))
            ViewData["MetaDescription"] = description;
        if (!string.IsNullOrWhiteSpace(canonicalPath))
            ViewData["Canonical"] = SitePaths.Absolute(SiteUrl, canonicalPath);
        if (!string.IsNullOrWhiteSpace(ogImage))
        {
            ogImage = ContentImageCatalog.Resolve(ogImage);
            ViewData["OgImage"] = ogImage.StartsWith("http", StringComparison.OrdinalIgnoreCase)
                ? ogImage
                : SiteUrl + ogImage;
        }
        ViewData["OgType"] = ogType;
        if (!string.IsNullOrWhiteSpace(robots))
            ViewData["Robots"] = robots;
        if (!string.IsNullOrWhiteSpace(hreflangTr))
            ViewData["HreflangTr"] = SitePaths.Absolute(SiteUrl, hreflangTr);
        if (!string.IsNullOrWhiteSpace(hreflangEn))
            ViewData["HreflangEn"] = SitePaths.Absolute(SiteUrl, hreflangEn);
    }

    protected async Task LoadContentAsync(string? section = null)
    {
        CurrentCulture = SitePaths.DetectCulture(HttpContext.Request.Path);
        Contents = await Cms.GetContentsAsync(section, CurrentCulture);
        SiteSettings = await Settings.GetSettingsAsync();
    }

    protected async Task LoadAllContentAsync()
    {
        CurrentCulture = SitePaths.DetectCulture(HttpContext.Request.Path);
        Contents = await Cms.GetContentsAsync(null, CurrentCulture);
        SiteSettings = await Settings.GetSettingsAsync();
    }
}
