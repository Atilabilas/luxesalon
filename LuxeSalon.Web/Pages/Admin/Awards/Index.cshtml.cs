using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LuxeSalon.Web.Pages.Admin.Awards;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context) => _context = context;

    public List<AwardItem> Items { get; set; } = new();

    public async Task OnGetAsync()
    {
        Items = await _context.AwardItems.OrderBy(a => a.DisplayOrder).ToListAsync();
    }
}
