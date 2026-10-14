using System.Globalization;
using System.Text.RegularExpressions;

namespace LuxeSalon.Web.Services;

public static class SitePaths
{
    public const string Tr = "tr";
    public const string En = "en";

    public static string DetectCulture(PathString path)
    {
        var value = path.Value ?? "/";
        return value.Equals("/en", StringComparison.OrdinalIgnoreCase) ||
               value.StartsWith("/en/", StringComparison.OrdinalIgnoreCase)
            ? En
            : Tr;
    }

    public static string StripCulturePrefix(string path)
    {
        if (string.IsNullOrWhiteSpace(path)) return "/";
        if (path.Equals("/en", StringComparison.OrdinalIgnoreCase)) return "/";
        if (path.StartsWith("/en/", StringComparison.OrdinalIgnoreCase))
            return path[3..];
        return path;
    }

    public static string WithCulture(string culture, string path)
    {
        path ??= "/";
        if (!path.StartsWith('/')) path = "/" + path;
        path = path.TrimEnd('/');
        if (string.IsNullOrEmpty(path)) path = "/";

        if (culture.Equals(En, StringComparison.OrdinalIgnoreCase))
            return path == "/" ? "/en" : "/en" + path;

        return path;
    }

    public static string Home(string culture) => WithCulture(culture, "/");
    public static string About(string culture) => WithCulture(culture, culture == En ? "/about" : "/hakkimizda");
    public static string Services(string culture) => WithCulture(culture, culture == En ? "/services" : "/hizmetler");
    public static string Service(string culture, string slug) => WithCulture(culture, (culture == En ? "/services/" : "/hizmetler/") + slug);
    public static string Blog(string culture) => WithCulture(culture, "/blog");
    public static string BlogPost(string culture, string slug) => WithCulture(culture, "/blog/" + slug);
    public static string Appointment(string culture) => WithCulture(culture, culture == En ? "/appointment" : "/randevu");
    public static string Privacy(string culture) => WithCulture(culture, culture == En ? "/privacy" : "/gizlilik");
    public static string Districts(string culture) => WithCulture(culture, culture == En ? "/izmir-districts" : "/izmir-ilceleri");
    public static string Shop(string culture) => WithCulture(culture, culture == En ? "/shop" : "/magaza");

    public static string Absolute(string siteUrl, string path) =>
        siteUrl.TrimEnd('/') + (path.StartsWith('/') ? path : "/" + path);
}

public static class LegacyPathMap
{
    private static readonly Dictionary<string, Func<string, string>> Exact = new(StringComparer.OrdinalIgnoreCase)
    {
        ["/Hakkimizda"] = c => SitePaths.About(c),
        ["/hakkimizda"] = c => SitePaths.About(c),
        ["/Hizmetler"] = c => SitePaths.Services(c),
        ["/hizmetler"] = c => SitePaths.Services(c),
        ["/Blog"] = c => SitePaths.Blog(c),
        ["/Randevu"] = c => SitePaths.Appointment(c),
        ["/randevu"] = c => SitePaths.Appointment(c),
        ["/Privacy"] = c => SitePaths.Privacy(c),
        ["/privacy"] = c => SitePaths.Privacy(c),
        ["/gizlilik"] = c => SitePaths.Privacy(c),
        ["/Shop"] = c => SitePaths.Shop(c),
        ["/shop"] = c => SitePaths.Shop(c),
        ["/magaza"] = c => SitePaths.Shop(c),
        ["/About"] = c => SitePaths.About("en"),
        ["/about"] = c => SitePaths.About("en"),
        ["/Services"] = c => SitePaths.Services("en"),
        ["/services"] = c => SitePaths.Services("en"),
        ["/Appointment"] = c => SitePaths.Appointment("en"),
        ["/appointment"] = c => SitePaths.Appointment("en"),
    };

    public static bool TryMap(string path, out string? target)
    {
        target = null;
        if (Exact.TryGetValue(path, out var map))
        {
            var culture = path.Contains("about", StringComparison.OrdinalIgnoreCase) ||
                          path.Contains("services", StringComparison.OrdinalIgnoreCase) ||
                          path.Contains("appointment", StringComparison.OrdinalIgnoreCase) ||
                          path.Equals("/Privacy", StringComparison.OrdinalIgnoreCase) ||
                          path.Equals("/privacy", StringComparison.OrdinalIgnoreCase) ||
                          path.Equals("/Shop", StringComparison.OrdinalIgnoreCase) ||
                          path.Equals("/shop", StringComparison.OrdinalIgnoreCase)
                ? "en"
                : "tr";

            // Prefer Turkish for Turkish legacy paths
            if (path.Contains("Hakkimizda", StringComparison.OrdinalIgnoreCase) ||
                path.Contains("Hizmetler", StringComparison.OrdinalIgnoreCase) ||
                path.Contains("Randevu", StringComparison.OrdinalIgnoreCase) ||
                path.Equals("/gizlilik", StringComparison.OrdinalIgnoreCase) ||
                path.Equals("/magaza", StringComparison.OrdinalIgnoreCase) ||
                path.Equals("/Blog", StringComparison.OrdinalIgnoreCase) ||
                path.Equals("/blog", StringComparison.OrdinalIgnoreCase))
            {
                culture = "tr";
            }

            // English named paths without /en prefix should go to /en/*
            if (Regex.IsMatch(path, @"^/(about|services|appointment|privacy|shop)$", RegexOptions.IgnoreCase))
                culture = "en";

            target = map(culture);
            // Avoid redirect loop for already-canonical Turkish paths that Exact also lists
            if (string.Equals(path, target, StringComparison.Ordinal))
            {
                target = null;
                return false;
            }
            return true;
        }

        return false;
    }
}
