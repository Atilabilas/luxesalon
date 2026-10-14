using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace LuxeSalon.Core.Utilities;

public static class SlugHelper
{
    private static readonly Dictionary<char, string> TurkishMap = new()
    {
        ['ç'] = "c", ['Ç'] = "c",
        ['ğ'] = "g", ['Ğ'] = "g",
        ['ı'] = "i", ['İ'] = "i",
        ['ö'] = "o", ['Ö'] = "o",
        ['ş'] = "s", ['Ş'] = "s",
        ['ü'] = "u", ['Ü'] = "u",
        ['I'] = "i"
    };

    public static string FromTitle(string title, int maxLength = 120)
    {
        if (string.IsNullOrWhiteSpace(title))
            return string.Empty;

        var sb = new StringBuilder(title.Length);
        foreach (var ch in title.Normalize(NormalizationForm.FormC))
        {
            if (TurkishMap.TryGetValue(ch, out var mapped))
            {
                sb.Append(mapped);
                continue;
            }

            var lower = char.ToLowerInvariant(ch);
            if ((lower >= 'a' && lower <= 'z') || (lower >= '0' && lower <= '9'))
            {
                sb.Append(lower);
            }
            else if (char.IsWhiteSpace(ch) || ch is '-' or '_' or '.' or ',' or '/' or '&')
            {
                sb.Append('-');
            }
        }

        var slug = Regex.Replace(sb.ToString(), "-{2,}", "-").Trim('-');
        if (slug.Length > maxLength)
            slug = slug[..maxLength].TrimEnd('-');

        return slug;
    }

    public static string EnsureUnique(string baseSlug, IEnumerable<string> existingSlugs, int? idFallback = null)
    {
        var set = new HashSet<string>(existingSlugs.Where(s => !string.IsNullOrWhiteSpace(s)), StringComparer.OrdinalIgnoreCase);
        if (string.IsNullOrWhiteSpace(baseSlug))
            baseSlug = idFallback.HasValue ? $"yazi-{idFallback.Value}" : "yazi";

        if (!set.Contains(baseSlug))
            return baseSlug;

        for (var i = 2; i < 1000; i++)
        {
            var candidate = $"{baseSlug}-{i}";
            if (!set.Contains(candidate))
                return candidate;
        }

        return idFallback.HasValue ? $"{baseSlug}-{idFallback.Value}" : $"{baseSlug}-{Guid.NewGuid():N}"[..Math.Min(120, baseSlug.Length + 20)];
    }

    public static string CulturePrefix(string culture) =>
        string.Equals(culture, "en", StringComparison.OrdinalIgnoreCase) ? "/en" : "";
}
