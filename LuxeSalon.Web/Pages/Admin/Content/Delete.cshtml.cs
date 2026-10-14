using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LuxeSalon.Web.Pages.Admin.Content;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public SiteContent SiteContent { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var content = await _context.SiteContents.FindAsync(id);
        if (content == null) return NotFound();
        SiteContent = content;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var content = await _context.SiteContents.FindAsync(id);
        if (content != null)
        {
            _context.SiteContents.Remove(content);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage("Index");
    }
}
