using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Web.Pages;
using LuxeSalon.Web.Services;
using Microsoft.EntityFrameworkCore;

namespace LuxeSalon.Web.Pages.Shop;

public class IndexModel : PageContentModel
{
    public IndexModel(ApplicationDbContext context, ContentService content, SiteSettingsService settings)
        : base(context, content, settings) { }

    public SiteImage? HeroImage { get; set; }
    public IList<Product> Products { get; set; } = new List<Product>();

    public async Task OnGetAsync()
    {
        await LoadContentAsync("Shop");

        HeroImage = await Cms.GetImageAsync("Hero_Shop");
        Products = await Context.Products.ToListAsync();
    }
}
