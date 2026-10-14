using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Web.Services;
using Microsoft.EntityFrameworkCore;

namespace LuxeSalon.Web.Pages;

public class IndexModel : PageContentModel
{
    public IndexModel(ApplicationDbContext context, ContentService content, SiteSettingsService settings)
        : base(context, content, settings) { }

    public SiteImage? HeroImage { get; set; }
    public List<Service> FeaturedServices { get; set; } = new();
    public List<Service> LandingServices { get; set; } = new();

    public async Task OnGetAsync()
    {
        await LoadAllContentAsync();
        HeroImage = await Context.SiteImages.AsNoTracking()
            .Where(i => i.IsActive && i.Section == "Hero")
            .OrderBy(i => i.DisplayOrder).FirstOrDefaultAsync();
        LandingServices = ServiceSearchCopy.Sort(await Context.Services.AsNoTracking()
            .Where(s => s.IsPublished && s.IsLandingPage && s.Slug != null && s.Slug != "")
            .ToListAsync());
        if (IsEnglish) LandingServices = LandingServices.Where(s => s.HasEnglish).ToList();
        FeaturedServices = LandingServices
            .Where(s => s.Slug is "kadin-sac-kesimi" or "balyaj-ombre-sombre" or "keratin-sac-botoksu")
            .ToList();
    }
}
