using System.Text.RegularExpressions;
using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Web.Services;
using Microsoft.EntityFrameworkCore;

namespace LuxeSalon.Web.Middleware;

public class CanonicalRedirectMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly Regex BlogId = new(@"^/(?:en/)?blog/(?:details/)?(\d+)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex ServiceId = new(@"^/(?:en/)?(?:hizmetler|services)/(?:details/)?(\d+)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex Detail = new(@"^/(en/)?(blog|hizmetler|services)/([^/]+)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public CanonicalRedirectMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context, ApplicationDbContext db)
    {
        var request = context.Request;
        var original = request.Path.Value ?? "/";
        if ((!HttpMethods.IsGet(request.Method) && !HttpMethods.IsHead(request.Method)) ||
            original.StartsWith("/Admin", StringComparison.OrdinalIgnoreCase) ||
            original.StartsWith("/api/", StringComparison.OrdinalIgnoreCase))
        {
            await _next(context);
            return;
        }

        var path = original.Length > 1 ? original.TrimEnd('/') : original;
        var culture = SitePaths.DetectCulture(path);
        var blogId = BlogId.Match(path);
        var serviceId = ServiceId.Match(path);

        if (blogId.Success)
        {
            if (!int.TryParse(blogId.Groups[1].Value, out var id))
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }
            var post = await db.BlogPosts.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id && p.IsPublished);
            if (post == null || string.IsNullOrWhiteSpace(post.Slug))
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }
            culture = culture == "en" && post.HasEnglish ? "en" : "tr";
            path = SitePaths.BlogPost(culture, post.LocalizedSlug(culture));
        }
        else if (serviceId.Success)
        {
            if (!int.TryParse(serviceId.Groups[1].Value, out var id))
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }
            var service = await db.Services.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id && s.IsPublished && s.IsLandingPage);
            if (service == null || string.IsNullOrWhiteSpace(service.Slug))
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }
            culture = culture == "en" && service.HasEnglish ? "en" : "tr";
            path = SitePaths.Service(culture, service.LocalizedSlug(culture));
        }
        else if (path.Equals("/Blog/Index", StringComparison.OrdinalIgnoreCase))
            path = SitePaths.Blog("tr");
        else if (path.Equals("/en/blog/index", StringComparison.OrdinalIgnoreCase))
            path = SitePaths.Blog("en");
        else if (LegacyPathMap.TryMap(path, out var mapped) && mapped != null)
            path = mapped;
        else
        {
            var detail = Detail.Match(path);
            if (detail.Success)
            {
                var slug = detail.Groups[3].Value;
                if (detail.Groups[2].Value.Equals("blog", StringComparison.OrdinalIgnoreCase))
                {
                    var post = await db.BlogPosts.AsNoTracking().FirstOrDefaultAsync(p => p.IsPublished &&
                        (EF.Functions.Collate(p.Slug, "NOCASE") == slug || EF.Functions.Collate(p.SlugEn!, "NOCASE") == slug));
                    if (post != null)
                    {
                        culture = culture == "en" && post.HasEnglish ? "en" : "tr";
                        path = SitePaths.BlogPost(culture, post.LocalizedSlug(culture));
                    }
                }
                else
                {
                    var service = await db.Services.AsNoTracking().FirstOrDefaultAsync(s => s.IsPublished && s.IsLandingPage &&
                        (EF.Functions.Collate(s.Slug, "NOCASE") == slug || EF.Functions.Collate(s.SlugEn!, "NOCASE") == slug));
                    if (service != null)
                    {
                        culture = culture == "en" && service.HasEnglish ? "en" : "tr";
                        path = SitePaths.Service(culture, service.LocalizedSlug(culture));
                    }
                }
            }
            else if (!Path.HasExtension(path) && !path.StartsWith("/uploads/", StringComparison.OrdinalIgnoreCase))
                path = path.ToLowerInvariant();
        }

        var siteUrl = await db.SiteSettings.AsNoTracking().Where(s => s.Key == "Site_Url").Select(s => s.Value).FirstOrDefaultAsync()
            ?? "https://celilatilla.com";
        var root = new Uri(siteUrl.TrimEnd('/'));
        var isSiteHost = request.Host.Host.Equals(root.Host, StringComparison.OrdinalIgnoreCase) ||
                         request.Host.Host.Equals("www." + root.Host, StringComparison.OrdinalIgnoreCase);
        var origin = isSiteHost ? root.GetLeftPart(UriPartial.Authority) : $"{request.Scheme}://{request.Host}";
        var currentOrigin = $"{request.Scheme}://{request.Host}";

        if (!string.Equals(path, original, StringComparison.Ordinal) || !string.Equals(origin, currentOrigin, StringComparison.OrdinalIgnoreCase))
        {
            context.Response.StatusCode = StatusCodes.Status301MovedPermanently;
            context.Response.Headers.Location = origin + path + request.QueryString;
            return;
        }

        await _next(context);
    }
}
