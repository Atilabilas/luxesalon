using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Web.Pages;
using LuxeSalon.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LuxeSalon.Web.Pages.Shop;

public class DetailsModel : PageContentModel
{
    public DetailsModel(ApplicationDbContext context, ContentService content, SiteSettingsService settings)
        : base(context, content, settings) { }

    public Product Product { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        await LoadContentAsync("Shop");

        var product = await Context.Products.FirstOrDefaultAsync(m => m.Id == id);
        if (product == null)
            return NotFound();

        Product = product;
        return Page();
    }
}
