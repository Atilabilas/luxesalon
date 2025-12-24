using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LuxeSalon.Web.Pages.Admin.Blog;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<BlogPost> BlogPosts { get; set; } = default!;

    public async Task OnGetAsync()
    {
        BlogPosts = await _context.BlogPosts.OrderByDescending(b => b.PublishedAt).ToListAsync();
    }
}
