using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LuxeSalon.Web.Pages.Admin.Services;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Service> Services { get; set; } = new();

    public async Task OnGetAsync()
    {
        Services = await _context.Services.OrderBy(s => s.Category).ThenBy(s => s.Name).ToListAsync();
    }
}
