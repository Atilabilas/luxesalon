using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LuxeSalon.Web.Pages.Admin.Marquee;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context) => _context = context;

    [BindProperty]
    public MarqueeItem MarqueeItem { get; set; } = new() { IsActive = true };

    public async Task<IActionResult> OnPostAsync()
    {
        MarqueeItem.UpdatedAt = DateTime.UtcNow;
        _context.MarqueeItems.Add(MarqueeItem);
        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
