using System.Text.Json;
using LuxeSalon.Core.Entities;

namespace LuxeSalon.Web.Services;

public static class JsonLdService
{
    public static string HairSalon(IReadOnlyDictionary<string, string> settings)
    {
        var siteUrl = settings.GetValueOrDefault("Site_Url", "https://celilatilla.com").TrimEnd('/');
        var instagram = settings.GetValueOrDefault("Social_Instagram", "");
        var maps = settings.GetValueOrDefault("Map_Google_Url", "");
        var sameAs = new[] { instagram, maps }.Where(u => !string.IsNullOrWhiteSpace(u)).ToArray();

        var payload = new Dictionary<string, object?>
        {
            ["@context"] = "https://schema.org",
            ["@type"] = new[] { "HairSalon", "BeautySalon", "LocalBusiness" },
            ["@id"] = $"{siteUrl}/#salon",
            ["name"] = settings.GetValueOrDefault("Site_Name", "Celil & Atilla"),
            ["alternateName"] = "Celil Atilla",
            ["url"] = siteUrl,
            ["image"] = $"{siteUrl}/images/logo.png",
            ["logo"] = new Dictionary<string, object>
            {
                ["@type"] = "ImageObject",
                ["url"] = $"{siteUrl}/images/logo.png"
            },
            ["description"] = settings.GetValueOrDefault("SEO_Description", ""),
            ["address"] = new Dictionary<string, string>
            {
                ["@type"] = "PostalAddress",
                ["streetAddress"] = settings.GetValueOrDefault("Contact_Address", ""),
                ["addressLocality"] = "Bornova",
                ["addressRegion"] = "İzmir",
                ["postalCode"] = "35100",
                ["addressCountry"] = "TR"
            },
            ["hasMap"] = maps,
            ["areaServed"] = new object[]
            {
                new Dictionary<string, string> { ["@type"] = "City", ["name"] = "Bornova" },
                new Dictionary<string, string> { ["@type"] = "AdministrativeArea", ["name"] = "İzmir" },
                new Dictionary<string, string> { ["@type"] = "Place", ["name"] = "Kazımdirik" }
            },
            ["knowsAbout"] = new[]
            {
                "Kadın saç kesimi", "Balyaj", "Ombre", "Sombre", "Keratin bakımı",
                "Saç botoksu", "Erkek fade", "Gelin saçı", "Manikür"
            }
        };

        if (settings.TryGetValue("Contact_Phone", out var phone) && !string.IsNullOrWhiteSpace(phone))
            payload["telephone"] = phone.Trim();
        else if (!settings.ContainsKey("Contact_Phone"))
            payload["telephone"] = "+905347944393";

        if (MapCoordinates.TryParse(settings.GetValueOrDefault("Map_Lat", "38.46234930909974"), -90, 90, out var latitude) &&
            MapCoordinates.TryParse(settings.GetValueOrDefault("Map_Lng", "27.208800699999998"), -180, 180, out var longitude))
        {
            payload["geo"] = new Dictionary<string, object>
            {
                ["@type"] = "GeoCoordinates",
                ["latitude"] = latitude,
                ["longitude"] = longitude
            };
        }

        var hours = ContactHours.Parse(settings.GetValueOrDefault("Contact_Hours", ContactHours.DefaultText));
        if (hours is { Length: > 0 })
            payload["openingHoursSpecification"] = hours;

        if (sameAs.Length > 0)
            payload["sameAs"] = sameAs;

        return JsonSerializer.Serialize(payload);
    }

    public static string FaqPage(IEnumerable<(string Question, string Answer)> items)
    {
        var entities = items
            .Where(x => !string.IsNullOrWhiteSpace(x.Question) && !string.IsNullOrWhiteSpace(x.Answer))
            .Select(x => new Dictionary<string, object>
            {
                ["@type"] = "Question",
                ["name"] = x.Question,
                ["acceptedAnswer"] = new Dictionary<string, string>
                {
                    ["@type"] = "Answer",
                    ["text"] = x.Answer
                }
            })
            .ToArray();

        var payload = new Dictionary<string, object?>
        {
            ["@context"] = "https://schema.org",
            ["@type"] = "FAQPage",
            ["mainEntity"] = entities
        };
        return JsonSerializer.Serialize(payload);
    }

    public static string Breadcrumb(string pageName, string pagePath, IReadOnlyDictionary<string, string> settings)
        => Breadcrumb(new[] { (pageName, pagePath) }, settings);

    public static string Breadcrumb(IEnumerable<(string Name, string Path)> crumbs, IReadOnlyDictionary<string, string> settings)
    {
        var siteUrl = settings.GetValueOrDefault("Site_Url", "https://celilatilla.com").TrimEnd('/');
        var crumbList = crumbs.ToArray();
        var culture = SitePaths.DetectCulture(crumbList.FirstOrDefault().Path ?? "/");
        var items = new List<object>
        {
            new Dictionary<string, object>
            {
                ["@type"] = "ListItem",
                ["position"] = 1,
                ["name"] = culture == "en" ? "Home" : "Ana Sayfa",
                ["item"] = siteUrl + SitePaths.Home(culture)
            }
        };

        var position = 2;
        foreach (var crumb in crumbList)
        {
            var path = crumb.Path.StartsWith("http", StringComparison.OrdinalIgnoreCase)
                ? crumb.Path
                : siteUrl + (crumb.Path.StartsWith('/') ? crumb.Path : "/" + crumb.Path);
            items.Add(new Dictionary<string, object>
            {
                ["@type"] = "ListItem",
                ["position"] = position++,
                ["name"] = crumb.Name,
                ["item"] = path
            });
        }

        var payload = new Dictionary<string, object?>
        {
            ["@context"] = "https://schema.org",
            ["@type"] = "BreadcrumbList",
            ["itemListElement"] = items
        };
        return JsonSerializer.Serialize(payload);
    }

    public static string BlogPosting(
        string title,
        string summary,
        string url,
        string? imageUrl,
        DateTime published,
        DateTime updated,
        string author,
        IReadOnlyDictionary<string, string> settings)
    {
        var siteUrl = settings.GetValueOrDefault("Site_Url", "https://celilatilla.com").TrimEnd('/');
        var siteName = settings.GetValueOrDefault("Site_Name", "Celil & Atilla");
        var absoluteUrl = url.StartsWith("http", StringComparison.OrdinalIgnoreCase) ? url : siteUrl + url;
        var absoluteImage = AbsoluteImage(siteUrl, imageUrl);

        var payload = new Dictionary<string, object?>
        {
            ["@context"] = "https://schema.org",
            ["@type"] = "BlogPosting",
            ["headline"] = title,
            ["description"] = summary,
            ["datePublished"] = published.ToString("yyyy-MM-dd"),
            ["dateModified"] = (updated < published ? published : updated).ToString("yyyy-MM-dd"),
            ["author"] = new Dictionary<string, string>
            {
                ["@type"] = IsSalonAuthor(author, siteName) ? "Organization" : "Person",
                ["name"] = author,
                ["url"] = siteUrl + SitePaths.About(SitePaths.DetectCulture(new Uri(absoluteUrl).AbsolutePath)) +
                    (string.Equals(author, "Celil", StringComparison.OrdinalIgnoreCase) ? "#celil" : "#ekip")
            },
            ["publisher"] = new Dictionary<string, object>
            {
                ["@type"] = "Organization",
                ["name"] = siteName,
                ["url"] = siteUrl,
                ["logo"] = new Dictionary<string, object>
                {
                    ["@type"] = "ImageObject",
                    ["url"] = $"{siteUrl}/images/logo.png"
                }
            },
            ["mainEntityOfPage"] = absoluteUrl,
            ["inLanguage"] = absoluteUrl.Contains("/en/", StringComparison.OrdinalIgnoreCase) ? "en-US" : "tr-TR"
        };
        if (absoluteImage != null)
            payload["image"] = absoluteImage;
        return JsonSerializer.Serialize(payload);
    }

    private static bool IsSalonAuthor(string author, string siteName) =>
        string.Equals(author?.Trim(), siteName?.Trim(), StringComparison.OrdinalIgnoreCase) ||
        string.Equals(author?.Trim(), "Celil & Atilla", StringComparison.OrdinalIgnoreCase);

    private static string? AbsoluteImage(string siteUrl, string? imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            return null;
        var resolved = ContentImageCatalog.Resolve(imageUrl.Trim());
        if (string.IsNullOrWhiteSpace(resolved))
            return null;
        return resolved.StartsWith("http", StringComparison.OrdinalIgnoreCase) ? resolved : siteUrl + resolved;
    }

    public static string ServiceSchema(Service service, string culture, IReadOnlyDictionary<string, string> settings)
    {
        var siteUrl = settings.GetValueOrDefault("Site_Url", "https://celilatilla.com").TrimEnd('/');
        var path = SitePaths.Service(culture, service.LocalizedSlug(culture));
        var payload = new Dictionary<string, object?>
        {
            ["@context"] = "https://schema.org",
            ["@type"] = "Service",
            ["name"] = ServiceSearchCopy.VisibleName(service, culture),
            ["description"] = ServiceSearchCopy.Meta(service, culture),
            ["url"] = siteUrl + path,
            ["provider"] = new Dictionary<string, string>
            {
                ["@type"] = "HairSalon",
                ["@id"] = $"{siteUrl}/#salon",
                ["name"] = settings.GetValueOrDefault("Site_Name", "Celil & Atilla")
            },
            ["areaServed"] = new Dictionary<string, string>
            {
                ["@type"] = "City",
                ["name"] = "Bornova"
            }
        };
        var serviceImage = AbsoluteImage(siteUrl, service.ImageUrl);
        if (serviceImage != null)
            payload["image"] = serviceImage;
        return JsonSerializer.Serialize(payload);
    }

    public static string ServiceIndex(IEnumerable<Service> services, string culture, IReadOnlyDictionary<string, string> settings)
    {
        var siteUrl = settings.GetValueOrDefault("Site_Url", "https://celilatilla.com").TrimEnd('/');
        var position = 1;
        var items = ServiceSearchCopy.Sort(services)
            .Where(service => !string.IsNullOrWhiteSpace(service.LocalizedSlug(culture)))
            .Select(service => new Dictionary<string, object>
            {
                ["@type"] = "ListItem",
                ["position"] = position++,
                ["name"] = ServiceSearchCopy.VisibleName(service, culture),
                ["url"] = siteUrl + SitePaths.Service(culture, service.LocalizedSlug(culture))
            })
            .ToArray();

        var payload = new Dictionary<string, object?>
        {
            ["@context"] = "https://schema.org",
            ["@type"] = "ItemList",
            ["name"] = culture.StartsWith("en", StringComparison.OrdinalIgnoreCase)
                ? ServiceSearchCopy.DirectoryTitleEn
                : ServiceSearchCopy.DirectoryTitleTr,
            ["itemListElement"] = items
        };
        return JsonSerializer.Serialize(payload);
    }
}
