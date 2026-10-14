using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LuxeSalon.Web.Pages.Admin.Marquee;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context) => _context = context;

    [BindProperty]
    public MarqueeItem MarqueeItem { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var item = await _context.MarqueeItems.FindAsync(id);
        if (item == null) return NotFound();
        MarqueeItem = item;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        MarqueeItem.UpdatedAt = DateTime.UtcNow;
        _context.MarqueeItems.Update(MarqueeItem);
        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
