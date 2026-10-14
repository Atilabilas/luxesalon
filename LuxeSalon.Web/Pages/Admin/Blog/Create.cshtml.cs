using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Core.Entities;
using LuxeSalon.Core.Utilities;
using Microsoft.EntityFrameworkCore;

namespace LuxeSalon.Web.Pages.Admin.Blog;

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
    public BlogPost BlogPost { get; set; } = new();

    public void OnGet()
    {
        BlogPost.PublishedAt = DateTime.Now;
        BlogPost.Author = "Celil & Atilla";
    }

    public async Task<IActionResult> OnPostAsync(IFormFile? HeaderImage)
    {
        // Handle image upload
        if (HeaderImage != null && HeaderImage.Length > 0)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "blog");
            Directory.CreateDirectory(uploadsFolder);
            
            var uniqueFileName = $"{Guid.NewGuid()}_{HeaderImage.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await HeaderImage.CopyToAsync(stream);
            }
            
            BlogPost.HeaderImageUrl = $"/uploads/blog/{uniqueFileName}";
        }

        if (string.IsNullOrWhiteSpace(BlogPost.Title) || string.IsNullOrWhiteSpace(BlogPost.Content))
        {
            ModelState.AddModelError(string.Empty, "Başlık ve içerik olmadan yazı kaydedilemez.");
            if (string.IsNullOrWhiteSpace(BlogPost.Author))
                BlogPost.Author = "Celil & Atilla";
            return Page();
        }

        var taken = await _context.BlogPosts.AsNoTracking()
            .Select(p => new { p.Slug, p.SlugEn })
            .ToListAsync();
        BlogPost.Slug = SlugHelper.EnsureUnique(
            SlugHelper.FromTitle(BlogPost.Title),
            taken.SelectMany(p => new[] { p.Slug, p.SlugEn }).OfType<string>());
        if (string.IsNullOrWhiteSpace(BlogPost.Author))
            BlogPost.Author = "Celil & Atilla";
        BlogPost.PublishedAt = DateTime.Now;
        BlogPost.UpdatedAt = DateTime.UtcNow;

        _context.BlogPosts.Add(BlogPost);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
