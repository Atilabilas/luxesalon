using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Core.Entities;

namespace LuxeSalon.Web.Pages.Admin.SiteImages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<SiteImage> LoaderImages { get; set; } = new();
    public SiteImage? HeroImage { get; set; }
    public SiteImage? AboutImage { get; set; }
    public List<SiteImage> ServiceImages { get; set; } = new();
    public SiteImage? PricingImage { get; set; }
    public SiteImage? MarqueeImage { get; set; }

    public async Task OnGetAsync()
    {
        var allImages = await _context.SiteImages.Where(i => i.IsActive).ToListAsync();
        
        LoaderImages = allImages.Where(i => i.Section.StartsWith("Loader")).OrderBy(i => i.DisplayOrder).ToList();
        HeroImage = allImages.FirstOrDefault(i => i.Section == "Hero");
        AboutImage = allImages.FirstOrDefault(i => i.Section == "About");
        ServiceImages = allImages.Where(i => i.Section.StartsWith("Service")).OrderBy(i => i.DisplayOrder).ToList();
        PricingImage = allImages.FirstOrDefault(i => i.Section == "Pricing");
        MarqueeImage = allImages.FirstOrDefault(i => i.Section == "Marquee");
    }
}
