using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Core.Entities;

namespace LuxeSalon.Web.Pages.Admin.Pricing;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public PriceItem PriceItem { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var item = await _context.PriceItems.FindAsync(id);
        if (item == null) return NotFound();
        PriceItem = item;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        PriceItem.UpdatedAt = DateTime.UtcNow;
        _context.PriceItems.Update(PriceItem);
        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
