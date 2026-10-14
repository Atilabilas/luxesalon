using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LuxeSalon.Web.Pages.Admin.Gallery;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context) => _context = context;

    public List<GalleryItem> Items { get; set; } = new();

    public async Task OnGetAsync()
    {
        Items = await _context.GalleryItems.OrderBy(g => g.Section).ThenBy(g => g.DisplayOrder).ToListAsync();
    }
}
