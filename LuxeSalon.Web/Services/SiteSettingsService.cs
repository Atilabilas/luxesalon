using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace LuxeSalon.Web.Services;

public class SiteSettingsService
{
    private const string CacheKey = "SiteSettings";
    private readonly ApplicationDbContext _context;
    private readonly IMemoryCache _cache;

    public SiteSettingsService(ApplicationDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<Dictionary<string, string>> GetSettingsAsync()
    {
        return await _cache.GetOrCreateAsync(CacheKey, async entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromHours(1);
            var settings = await _context.SiteSettings.ToListAsync();
            return settings.ToDictionary(s => s.Key, s => s.Value);
        }) ?? new Dictionary<string, string>();
    }

    public async Task<string> GetSettingAsync(string key, string defaultValue = "")
    {
        var settings = await GetSettingsAsync();
        return settings.TryGetValue(key, out var value) ? value : defaultValue;
    }

    public async Task UpdateSettingAsync(string key, string value)
    {
        var setting = await _context.SiteSettings.FirstOrDefaultAsync(s => s.Key == key);
        if (setting == null)
        {
            setting = new SiteSetting { Key = key, Value = value, Group = "General" };
            _context.SiteSettings.Add(setting);
        }
        else
        {
            setting.Value = value;
            setting.UpdatedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
        _cache.Remove(CacheKey);
    }

    public async Task<IReadOnlyList<string>> UpdateSettingsAsync(Dictionary<string, string> updates)
    {
        var errors = MapCoordinates.Validate(updates);
        if (errors.Count > 0)
            return errors;

        foreach (var (key, value) in updates)
        {
            var setting = await _context.SiteSettings.FirstOrDefaultAsync(s => s.Key == key);
            if (setting == null)
            {
                _context.SiteSettings.Add(new SiteSetting { Key = key, Value = value, Group = "General" });
            }
            else
            {
                setting.Value = value;
                setting.UpdatedAt = DateTime.UtcNow;
            }
        }

        await _context.SaveChangesAsync();
        _cache.Remove(CacheKey);
        return Array.Empty<string>();
    }

    public void InvalidateCache() => _cache.Remove(CacheKey);
}
