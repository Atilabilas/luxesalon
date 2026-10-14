using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LuxeSalon.Web.Pages.Admin.Services;

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
    public Service Service { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var service = await _context.Services.FindAsync(id);
        if (service == null) return NotFound();
        Service = service;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(IFormFile? ImageFile)
    {
        if (ImageFile != null && ImageFile.Length > 0)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "services");
            Directory.CreateDirectory(uploadsFolder);
            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(ImageFile.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            await using var stream = new FileStream(filePath, FileMode.Create);
            await ImageFile.CopyToAsync(stream);
            Service.ImageUrl = $"/uploads/services/{uniqueFileName}";
        }

        var existing = await _context.Services.FindAsync(Service.Id);
        if (existing == null) return NotFound();

        existing.Name = Service.Name;
        existing.Description = Service.Description ?? string.Empty;
        existing.Price = Service.Price;
        existing.DurationMinutes = Service.DurationMinutes;
        existing.Category = Service.Category;
        existing.ImageUrl = Service.ImageUrl ?? existing.ImageUrl;
        existing.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
