using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Core.Entities;

namespace LuxeSalon.Web.Pages.Admin.Content;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public SiteContent SiteContent { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var content = await _context.SiteContents.FindAsync(id);
        if (content == null) return NotFound();
        SiteContent = content;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        SiteContent.UpdatedAt = DateTime.UtcNow;
        _context.SiteContents.Update(SiteContent);
        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
