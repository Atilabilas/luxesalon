using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LuxeSalon.Web.Pages.Admin.Stats;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context) => _context = context;

    [BindProperty]
    public StatItem StatItem { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var item = await _context.StatItems.FindAsync(id);
        if (item == null) return NotFound();
        StatItem = item;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        StatItem.UpdatedAt = DateTime.UtcNow;
        _context.StatItems.Update(StatItem);
        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
