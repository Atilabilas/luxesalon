using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LuxeSalon.Web.Pages.Blog;

public class DetailsModel : PageContentModel
{
    public DetailsModel(ApplicationDbContext context, ContentService content, SiteSettingsService settings)
        : base(context, content, settings) { }

    public BlogPost BlogPost { get; set; } = default!;
    public List<BlogPost> RelatedPosts { get; set; } = new();
    public List<Service> RelatedServices { get; set; } = new();
    public string DisplayTitle => BlogPost.LocalizedTitle(CurrentCulture);
    public string DisplaySummary => BlogPost.LocalizedSummary(CurrentCulture);
    public string DisplayContent => BlogPost.LocalizedContent(CurrentCulture);
    public DateTime DisplayUpdatedAt => new[] { BlogPost.UpdatedAt, BlogPost.PublishedAt, LocalSalonCopy.UpdatedAt }.Max();

    public async Task<IActionResult> OnGetAsync(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            return NotFound();

        await LoadContentAsync("Blog");

        var culture = CurrentCulture;
        BlogPost? post;
        if (culture == "en")
        {
            post = await Context.BlogPosts.FirstOrDefaultAsync(m =>
                m.IsPublished && m.SlugEn == slug);
            if (post == null)
            {
                // Wrong language slug → try TR and redirect
                post = await Context.BlogPosts.FirstOrDefaultAsync(m => m.IsPublished && m.Slug == slug);
                if (post?.HasEnglish == true)
                    return RedirectPermanent(SitePaths.BlogPost("en", post.SlugEn!));
                if (post != null)
                    return RedirectPermanent(SitePaths.BlogPost("tr", post.Slug));
                return NotFound();
            }
        }
        else
        {
            post = await Context.BlogPosts.FirstOrDefaultAsync(m => m.IsPublished && m.Slug == slug);
            if (post == null)
            {
                post = await Context.BlogPosts.FirstOrDefaultAsync(m => m.IsPublished && m.SlugEn == slug);
                if (post != null)
                    return RedirectPermanent(SitePaths.BlogPost("tr", post.Slug));
                return NotFound();
            }
        }

        BlogPost = post;

        // Canonicalize casing
        var canonicalSlug = post.LocalizedSlug(culture);
        if (!string.Equals(slug, canonicalSlug, StringComparison.Ordinal))
            return RedirectPermanent(SitePaths.BlogPost(culture, canonicalSlug));

        RelatedPosts = await GetRelatedAsync(post, culture);
        var tags = (post.Tags ?? "").Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToHashSet(StringComparer.OrdinalIgnoreCase);
        var services = await Context.Services.AsNoTracking().Where(s => s.IsPublished && s.IsLandingPage).ToListAsync();
        RelatedServices = services.Where(s => culture != "en" || s.HasEnglish)
            .Select(s => (Service: s, Score: (s.Tags ?? "").Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Count(tags.Contains)))
            .Where(s => s.Score > 0).OrderByDescending(s => s.Score).ThenBy(s => s.Service.Id).Take(2).Select(s => s.Service).ToList();
        return Page();
    }

    private async Task<List<BlogPost>> GetRelatedAsync(BlogPost post, string culture)
    {
        var tags = (post.Tags ?? "")
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(t => t.ToLowerInvariant())
            .ToHashSet();

        var candidates = await Context.BlogPosts
            .Where(p => p.IsPublished && p.Id != post.Id)
            .OrderByDescending(p => p.PublishedAt)
            .Take(20)
            .ToListAsync();

        var scored = candidates
            .Where(p => culture != "en" || p.HasEnglish)
            .Select(p =>
            {
                var score = 0;
                var pTags = (p.Tags ?? "").Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                score += pTags.Count(t => tags.Contains(t.ToLowerInvariant())) * 10;
                if (culture == "en" && !p.HasEnglish) score -= 5;
                return (Post: p, Score: score);
            })
            .OrderByDescending(x => x.Score)
            .ThenByDescending(x => x.Post.PublishedAt)
            .Take(3)
            .Select(x => x.Post)
            .ToList();

        return scored;
    }
}
