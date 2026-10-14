using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LuxeSalon.Web.Pages.Admin.Awards;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context) => _context = context;

    [BindProperty]
    public AwardItem AwardItem { get; set; } = new() { IsActive = true, Icon = "🏆" };

    public async Task<IActionResult> OnPostAsync()
    {
        AwardItem.UpdatedAt = DateTime.UtcNow;
        _context.AwardItems.Add(AwardItem);
        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
