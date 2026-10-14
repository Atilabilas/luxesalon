using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LuxeSalon.Web.Pages.Admin.SiteImages;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public SiteImage SiteImage { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var image = await _context.SiteImages.FindAsync(id);
        if (image == null) return NotFound();
        SiteImage = image;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var image = await _context.SiteImages.FindAsync(id);
        if (image != null)
        {
            _context.SiteImages.Remove(image);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage("Index");
    }
}
