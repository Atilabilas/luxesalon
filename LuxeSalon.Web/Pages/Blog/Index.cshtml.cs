using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Web.Services;
using Microsoft.EntityFrameworkCore;

namespace LuxeSalon.Web.Pages.Blog;

public class IndexModel : PageContentModel
{
    public IndexModel(ApplicationDbContext context, ContentService content, SiteSettingsService settings)
        : base(context, content, settings) { }

    public SiteImage? HeroImage { get; set; }
    public IList<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();

    public async Task OnGetAsync()
    {
        await LoadContentAsync("Blog");

        HeroImage = await Cms.GetImageAsync("Hero_Blog");
        var query = Context.BlogPosts.Where(b => b.IsPublished);
        if (CurrentCulture == "en")
            query = query.Where(b => b.TitleEn != null && b.TitleEn != "" && b.SlugEn != null && b.SlugEn != "" && b.ContentEn != null && b.ContentEn != "");

        BlogPosts = await query
            .OrderByDescending(b => b.PublishedAt)
            .ToListAsync();
    }
}
