using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Core.Entities;

namespace LuxeSalon.Web.Pages.Admin.SiteImages;

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
    public SiteImage SiteImage { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? id, string? section)
    {
        if (id.HasValue)
        {
            var image = await _context.SiteImages.FindAsync(id.Value);
            if (image == null) return NotFound();
            SiteImage = image;
        }
        else if (!string.IsNullOrEmpty(section))
        {
            SiteImage.Section = section;
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(IFormFile? ImageFile)
    {
        if (ImageFile != null && ImageFile.Length > 0)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "site");
            Directory.CreateDirectory(uploadsFolder);
            
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ImageFile.CopyToAsync(stream);
            }
            
            SiteImage.ImageUrl = "/uploads/site/" + uniqueFileName;
        }

        SiteImage.UpdatedAt = DateTime.UtcNow;

        if (SiteImage.Id == 0)
        {
            _context.SiteImages.Add(SiteImage);
        }
        else
        {
            _context.SiteImages.Update(SiteImage);
        }
        
        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
