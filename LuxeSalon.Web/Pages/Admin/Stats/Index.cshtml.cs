using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LuxeSalon.Web.Pages.Admin.Stats;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context) => _context = context;

    public List<StatItem> Items { get; set; } = new();

    public async Task OnGetAsync()
    {
        Items = await _context.StatItems.OrderBy(s => s.Section).ThenBy(s => s.DisplayOrder).ToListAsync();
    }
}
