using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LuxeSalon.Web.Pages.Admin.Testimonials;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context) => _context = context;

    public Testimonial Testimonial { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var item = await _context.Testimonials.FindAsync(id);
        if (item == null) return NotFound();
        Testimonial = item;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var item = await _context.Testimonials.FindAsync(id);
        if (item != null)
        {
            _context.Testimonials.Remove(item);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage("Index");
    }
}
