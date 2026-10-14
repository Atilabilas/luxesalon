using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LuxeSalon.Web.Pages.Admin.Awards;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context) => _context = context;

    [BindProperty]
    public AwardItem AwardItem { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var item = await _context.AwardItems.FindAsync(id);
        if (item == null) return NotFound();
        AwardItem = item;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        AwardItem.UpdatedAt = DateTime.UtcNow;
        _context.AwardItems.Update(AwardItem);
        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
