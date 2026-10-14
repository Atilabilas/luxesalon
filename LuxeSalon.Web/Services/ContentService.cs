using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LuxeSalon.Web.Services;

public class ContentService
{
    private readonly ApplicationDbContext _context;

    public ContentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Dictionary<string, SiteContent>> GetContentsAsync(string? section = null, string culture = "tr")
    {
        culture = NormalizeCulture(culture);
        var query = _context.SiteContents.AsQueryable();
        if (!string.IsNullOrEmpty(section))
            query = query.Where(c => c.Section == section);

        var list = await query.Where(c => c.Culture == culture || c.Culture == "tr" || c.Culture == "").ToListAsync();

        // Prefer requested culture; fall back to Turkish
        var preferred = list
            .GroupBy(c => c.Key)
            .Select(g =>
                g.FirstOrDefault(x => string.Equals(x.Culture, culture, StringComparison.OrdinalIgnoreCase))
                ?? g.FirstOrDefault(x => x.Culture == "tr" || string.IsNullOrEmpty(x.Culture))
                ?? g.First())
            .ToDictionary(c => c.Key, c => c);

        return preferred;
    }

    public async Task<string> GetContentAsync(string key, string fallback = "", string culture = "tr")
    {
        culture = NormalizeCulture(culture);
        var content = await _context.SiteContents
            .Where(c => c.Key == key && (c.Culture == culture || c.Culture == "tr" || c.Culture == ""))
            .OrderByDescending(c => c.Culture == culture)
            .FirstOrDefaultAsync();
        return content?.Value ?? fallback;
    }

    public int? GetContentId(Dictionary<string, SiteContent> contents, string key)
    {
        return contents.TryGetValue(key, out var content) ? content.Id : null;
    }

    public string GetContent(Dictionary<string, SiteContent> contents, string key, string fallback = "")
    {
        return contents.TryGetValue(key, out var content) ? content.Value : fallback;
    }

    public async Task<SiteImage?> GetImageAsync(string section)
    {
        return await _context.SiteImages
            .Where(i => i.IsActive && i.Section == section)
            .OrderBy(i => i.DisplayOrder)
            .FirstOrDefaultAsync();
    }

    public async Task<List<SiteImage>> GetImagesByPrefixAsync(string sectionPrefix)
    {
        return await _context.SiteImages
            .Where(i => i.IsActive && i.Section.StartsWith(sectionPrefix))
            .OrderBy(i => i.DisplayOrder)
            .ToListAsync();
    }

    public async Task<List<SiteImage>> GetImagesAsync(string section)
    {
        return await _context.SiteImages
            .Where(i => i.IsActive && i.Section == section)
            .OrderBy(i => i.DisplayOrder)
            .ToListAsync();
    }

    public static string NormalizeCulture(string? culture) =>
        string.Equals(culture, "en", StringComparison.OrdinalIgnoreCase) ? "en" : "tr";
}
