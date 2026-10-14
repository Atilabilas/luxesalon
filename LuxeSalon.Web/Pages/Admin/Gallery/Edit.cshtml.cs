using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LuxeSalon.Web.Pages.Admin.Gallery;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public EditModel(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    [BindProperty]
    public GalleryItem GalleryItem { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var item = await _context.GalleryItems.FindAsync(id);
        if (item == null) return NotFound();
        GalleryItem = item;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(IFormFile? ImageFile)
    {
        if (ImageFile != null && ImageFile.Length > 0)
        {
            var folder = Path.Combine(_environment.WebRootPath, "uploads", "gallery");
            Directory.CreateDirectory(folder);
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(ImageFile.FileName)}";
            await using var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create);
            await ImageFile.CopyToAsync(stream);
            GalleryItem.ImageUrl = $"/uploads/gallery/{fileName}";
        }

        GalleryItem.UpdatedAt = DateTime.UtcNow;
        _context.GalleryItems.Update(GalleryItem);
        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
