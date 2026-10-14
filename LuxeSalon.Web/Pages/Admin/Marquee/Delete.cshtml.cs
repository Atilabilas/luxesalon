using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LuxeSalon.Web.Pages.Admin.Marquee;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context) => _context = context;

    public MarqueeItem MarqueeItem { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var item = await _context.MarqueeItems.FindAsync(id);
        if (item == null) return NotFound();
        MarqueeItem = item;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var item = await _context.MarqueeItems.FindAsync(id);
        if (item != null)
        {
            _context.MarqueeItems.Remove(item);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage("Index");
    }
}
