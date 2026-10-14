using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LuxeSalon.Web.Pages.Admin.Content;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public SiteContent SiteContent { get; set; } = new() { ContentType = "text" };

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        SiteContent.UpdatedAt = DateTime.UtcNow;
        _context.SiteContents.Add(SiteContent);
        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
