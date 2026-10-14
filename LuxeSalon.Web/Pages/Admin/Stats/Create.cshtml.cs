using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LuxeSalon.Web.Pages.Admin.Stats;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context) => _context = context;

    [BindProperty]
    public StatItem StatItem { get; set; } = new() { Section = "Hakkimizda" };

    public async Task<IActionResult> OnPostAsync()
    {
        StatItem.UpdatedAt = DateTime.UtcNow;
        _context.StatItems.Add(StatItem);
        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
