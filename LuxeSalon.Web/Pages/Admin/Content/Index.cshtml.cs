using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Core.Entities;

namespace LuxeSalon.Web.Pages.Admin.Content;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<SiteContent> Contents { get; set; } = new();
    public List<string> Sections { get; set; } = new();

    public async Task OnGetAsync()
    {
        Contents = await _context.SiteContents.OrderBy(c => c.Section).ThenBy(c => c.DisplayOrder).ToListAsync();
        Sections = Contents.Select(c => c.Section).Distinct().ToList();
    }
}
