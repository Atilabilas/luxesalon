using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LuxeSalon.Web.Pages.Admin.Services;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public CreateModel(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    [BindProperty]
    public Service Service { get; set; } = new();

    public void OnGet()
    {
        Service.DurationMinutes = 60;
        Service.Category = "General";
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

        _context.Services.Add(Service);
        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
