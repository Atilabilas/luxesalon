using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Core.Entities;

namespace LuxeSalon.Web.Pages.Admin.Blog;

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
    public BlogPost BlogPost { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var blogPost = await _context.BlogPosts.FindAsync(id);
        if (blogPost == null)
        {
            return NotFound();
        }
        BlogPost = blogPost;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(IFormFile? HeaderImage)
    {
        var existingPost = await _context.BlogPosts.FindAsync(BlogPost.Id);
        if (existingPost == null)
        {
            return NotFound();
        }

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

        var imageUrl = BlogPost.HeaderImageUrl;
        var contentChanged =
            existingPost.Title != BlogPost.Title ||
            existingPost.Summary != BlogPost.Summary ||
            existingPost.Content != BlogPost.Content ||
            existingPost.HeaderImageUrl != imageUrl ||
            existingPost.IsPublished != BlogPost.IsPublished;

        existingPost.Title = BlogPost.Title;
        existingPost.Author = BlogPost.Author;
        existingPost.Summary = BlogPost.Summary;
        existingPost.Content = BlogPost.Content;
        existingPost.HeaderImageUrl = imageUrl;
        existingPost.IsPublished = BlogPost.IsPublished;
        if (contentChanged)
            existingPost.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
