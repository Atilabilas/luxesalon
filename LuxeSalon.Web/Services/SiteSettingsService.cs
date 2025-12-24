using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace LuxeSalon.Web.Services;

public class SiteSettingsService
{
    private readonly ApplicationDbContext _context;
    private readonly IMemoryCache _cache;

    public SiteSettingsService(ApplicationDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<Dictionary<string, string>> GetSettingsAsync()
    {
        return await _cache.GetOrCreateAsync("SiteSettings", async entry =>
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
}
