using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LuxeSalon.Web.Pages.Hizmetler;

public class DetailsModel : PageContentModel
{
    public DetailsModel(ApplicationDbContext context, ContentService content, SiteSettingsService settings)
        : base(context, content, settings) { }

    public Service Service { get; set; } = default!;
    public List<BlogPost> RelatedPosts { get; set; } = new();
    public List<Service> OtherLandings { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            return NotFound();

        await LoadContentAsync("Hizmetler");

        Service? service;
        if (CurrentCulture == "en")
        {
            service = await Context.Services.FirstOrDefaultAsync(s =>
                s.IsPublished && s.IsLandingPage && s.SlugEn == slug);
            if (service == null)
            {
                service = await Context.Services.FirstOrDefaultAsync(s =>
                    s.IsPublished && s.IsLandingPage && s.Slug == slug);
                if (service?.HasEnglish == true)
                    return RedirectPermanent(SitePaths.Service("en", service.SlugEn!));
                if (service != null)
                    return RedirectPermanent(SitePaths.Service("tr", service.Slug));
                return NotFound();
            }
        }
        else
        {
            service = await Context.Services.FirstOrDefaultAsync(s =>
                s.IsPublished && s.IsLandingPage && s.Slug == slug);
            if (service == null)
            {
                service = await Context.Services.FirstOrDefaultAsync(s =>
                    s.IsPublished && s.IsLandingPage && s.SlugEn == slug);
                if (service != null)
                    return RedirectPermanent(SitePaths.Service("tr", service.Slug));
                return NotFound();
            }
        }

        Service = service;
        OtherLandings = ServiceSearchCopy.Sort(await Context.Services.AsNoTracking()
            .Where(s => s.IsPublished && s.IsLandingPage && s.Id != service.Id && s.Slug != null && s.Slug != "")
            .ToListAsync());
        if (CurrentCulture == "en")
            OtherLandings = OtherLandings.Where(s => s.HasEnglish).ToList();

        var canonical = service.LocalizedSlug(CurrentCulture);
        if (!string.Equals(slug, canonical, StringComparison.Ordinal))
            return RedirectPermanent(SitePaths.Service(CurrentCulture, canonical));

        var tags = (service.Tags ?? "")
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(t => t.ToLowerInvariant())
            .ToHashSet();

        var posts = await Context.BlogPosts.Where(p => p.IsPublished).OrderByDescending(p => p.PublishedAt).Take(30).ToListAsync();
        RelatedPosts = posts
            .Where(p => CurrentCulture != "en" || p.HasEnglish)
            .Select(p =>
            {
                var pTags = (p.Tags ?? "").Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var score = pTags.Count(t => tags.Contains(t.ToLowerInvariant()));
                return (p, score);
            })
            .Where(x => x.score > 0)
            .OrderByDescending(x => x.score)
            .Take(3)
            .Select(x => x.p)
            .ToList();

        return Page();
    }
}
