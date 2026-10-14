using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LuxeSalon.Web.Pages.Admin.Testimonials;

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
    public Testimonial Testimonial { get; set; } = new() { IsActive = true };

    public async Task<IActionResult> OnPostAsync(IFormFile? ImageFile)
    {
        if (ImageFile != null && ImageFile.Length > 0)
        {
            var folder = Path.Combine(_environment.WebRootPath, "uploads", "testimonials");
            Directory.CreateDirectory(folder);
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(ImageFile.FileName)}";
            await using var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create);
            await ImageFile.CopyToAsync(stream);
            Testimonial.ImageUrl = $"/uploads/testimonials/{fileName}";
        }

        Testimonial.UpdatedAt = DateTime.UtcNow;
        _context.Testimonials.Add(Testimonial);
        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
