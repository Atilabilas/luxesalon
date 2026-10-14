using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LuxeSalon.Web.Pages.Admin.Testimonials;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context) => _context = context;

    public List<Testimonial> Testimonials { get; set; } = new();

    public async Task OnGetAsync()
    {
        Testimonials = await _context.Testimonials.OrderBy(t => t.DisplayOrder).ToListAsync();
    }
}
