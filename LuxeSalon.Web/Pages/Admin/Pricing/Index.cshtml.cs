using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Core.Entities;

namespace LuxeSalon.Web.Pages.Admin.Pricing;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<PriceItem> PriceItems { get; set; } = new();
    public List<string> Categories { get; set; } = new();

    public async Task OnGetAsync()
    {
        PriceItems = await _context.PriceItems.OrderBy(p => p.Category).ThenBy(p => p.DisplayOrder).ToListAsync();
        Categories = PriceItems.Select(p => p.Category).Distinct().ToList();
    }
}
