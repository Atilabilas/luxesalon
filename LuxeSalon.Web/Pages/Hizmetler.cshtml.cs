using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Web.Services;
using Microsoft.EntityFrameworkCore;

namespace LuxeSalon.Web.Pages;

public class HizmetlerModel : PageContentModel
{
    public HizmetlerModel(ApplicationDbContext context, ContentService content, SiteSettingsService settings)
        : base(context, content, settings) { }

    public SiteImage? HeroImage { get; set; }
    public List<Service> Services { get; set; } = new();

    public List<(string Category, string SubtitleKey, string TitleKey)> CategoryOrder { get; } =
    [
        ("Kadın Kesim", "hizmetler_cat_kadin_kesim", "hizmetler_cat_kadin_kesim_title"),
        ("Renklendirme", "hizmetler_cat_renk", "hizmetler_cat_renk_title"),
        ("Bakım", "hizmetler_cat_bakim", "hizmetler_cat_bakim_title"),
        ("Saç Kaynağı", "hizmetler_cat_kaynak", "hizmetler_cat_kaynak_title"),
        ("Tırnak", "hizmetler_cat_tirnak", "hizmetler_cat_tirnak_title"),
        ("Kaş, Kirpik & Ağda", "hizmetler_cat_kas", "hizmetler_cat_kas_title"),
        ("Özel Gün", "hizmetler_cat_ozel", "hizmetler_cat_ozel_title"),
        ("Erkek", "hizmetler_cat_erkek", "hizmetler_cat_erkek_title"),
        ("Çocuk", "hizmetler_cat_cocuk", "hizmetler_cat_cocuk_title"),
    ];

    public async Task OnGetAsync()
    {
        await LoadContentAsync("Hizmetler");

        HeroImage = await Cms.GetImageAsync("Hero_Hizmetler");
        var order = CategoryOrder.Select((c, i) => (c.Category, i)).ToDictionary(x => x.Category, x => x.i);
        Services = (await Context.Services.Where(s => s.IsPublished).ToListAsync())
            .OrderBy(s => order.GetValueOrDefault(s.Category, 99))
            .ThenBy(s => s.Name)
            .ToList();
    }
}
