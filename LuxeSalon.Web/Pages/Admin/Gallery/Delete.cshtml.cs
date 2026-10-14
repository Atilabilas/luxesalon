using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LuxeSalon.Web.Pages.Admin.Gallery;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context) => _context = context;

    public GalleryItem GalleryItem { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var item = await _context.GalleryItems.FindAsync(id);
        if (item == null) return NotFound();
        GalleryItem = item;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var item = await _context.GalleryItems.FindAsync(id);
        if (item != null)
        {
            _context.GalleryItems.Remove(item);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage("Index");
    }
}
