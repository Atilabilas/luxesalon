using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LuxeSalon.Web.Pages.Admin.Settings;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly SiteSettingsService _settingsService;

    public static readonly (string Group, string Label, string Key)[] SettingKeys =
    [
        ("General", "Site Adı", "Site_Name"),
        ("General", "Site Başlığı", "Site_Title"),
        ("SEO", "SEO Açıklaması", "SEO_Description"),
        ("Contact", "Telefon", "Contact_Phone"),
        ("Contact", "E-posta", "Contact_Email"),
        ("Contact", "Adres", "Contact_Address"),
        ("Contact", "Şehir", "Contact_City"),
        ("Contact", "Çalışma Saatleri", "Contact_Hours"),
        ("Contact", "WhatsApp Numarası", "Whatsapp_Number"),
        ("Contact", "WhatsApp Mesajı", "Whatsapp_Message"),
        ("Social", "Instagram URL", "Social_Instagram"),
        ("Social", "Facebook URL", "Social_Facebook"),
        ("Social", "Pinterest URL", "Social_Pinterest"),
        ("Social", "Instagram Kullanıcı Adı", "Instagram_Handle"),
        ("Map", "Harita Enlem", "Map_Lat"),
        ("Map", "Harita Boylam", "Map_Lng"),
        ("Map", "Harita Etiketi", "Map_Label"),
        ("Map", "Harita Adresi", "Map_Address"),
    ];

    public IndexModel(ApplicationDbContext context, SiteSettingsService settingsService)
    {
        _context = context;
        _settingsService = settingsService;
    }

    public Dictionary<string, List<SiteSetting>> GroupedSettings { get; set; } = new();

    [BindProperty]
    public Dictionary<string, string> FormValues { get; set; } = new();

    public string[] GroupOrder { get; } = ["General", "Contact", "Social", "SEO", "Map"];

    public async Task OnGetAsync()
    {
        var all = await LoadGroupsAsync();

        foreach (var (_, _, key) in SettingKeys)
        {
            var existing = all.FirstOrDefault(s => s.Key == key);
            FormValues[key] = existing?.Value ?? string.Empty;
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var errors = await _settingsService.UpdateSettingsAsync(FormValues);
        if (errors.Count > 0)
        {
            foreach (var error in errors)
                ModelState.AddModelError(string.Empty, error);
            await LoadGroupsAsync();
            return Page();
        }

        _settingsService.InvalidateCache();
        TempData["Success"] = "Ayarlar kaydedildi.";
        return RedirectToPage();
    }

    private async Task<List<SiteSetting>> LoadGroupsAsync()
    {
        var all = await _context.SiteSettings.OrderBy(s => s.Group).ThenBy(s => s.Key).ToListAsync();
        GroupedSettings = all.GroupBy(s => s.Group).ToDictionary(g => g.Key, g => g.ToList());
        return all;
    }
}
