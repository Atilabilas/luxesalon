using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace LuxeSalon.Web.Services;

public static class ContactHours
{
    public const string DefaultText = "Pzt - Cmt: 08:30 - 20:00\nPazar: 10:00 - 19:00";

    private static readonly string[] Week = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"];
    private static readonly Regex TimeRange = new(@"(\d{1,2}):(\d{2})\s*-\s*(\d{1,2}):(\d{2})", RegexOptions.CultureInvariant);
    private static readonly Regex Word = new(@"[a-z]+", RegexOptions.CultureInvariant);

    public static object[]? Parse(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return null;

        var normalized = text.Replace('–', '-').Replace('—', '-').Replace('\r', '\n');
        var hours = new Dictionary<int, (string Open, string Close)>();
        var closed = new HashSet<int>();
        (string Open, string Close)? unscoped = null;

        foreach (var line in normalized.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            var days = DayIndexes(line);
            var time = ReadTime(line);
            if (IsClosed(line) && time == null)
            {
                foreach (var day in days)
                {
                    closed.Add(day);
                    hours.Remove(day);
                }
                continue;
            }

            if (time == null)
                continue;
            if (days.Count == 0)
            {
                unscoped = time;
                continue;
            }

            foreach (var day in days)
            {
                closed.Remove(day);
                hours[day] = time.Value;
            }
        }

        if (unscoped != null)
        {
            foreach (var day in Enumerable.Range(0, 7))
            {
                if (!hours.ContainsKey(day) && !closed.Contains(day))
                    hours[day] = unscoped.Value;
            }
        }

        if (hours.Count == 0)
            return null;

        var groups = new List<(List<int> Days, string Open, string Close)>();
        foreach (var day in hours.Keys.OrderBy(d => d))
        {
            var span = hours[day];
            if (groups.Count > 0)
            {
                var last = groups[^1];
                if (last.Open == span.Open && last.Close == span.Close && last.Days[^1] == day - 1)
                {
                    last.Days.Add(day);
                    continue;
                }
            }

            groups.Add((new List<int> { day }, span.Open, span.Close));
        }

        return groups.Select(group => (object)new Dictionary<string, object>
        {
            ["@type"] = "OpeningHoursSpecification",
            ["dayOfWeek"] = group.Days.Count == 1 ? Week[group.Days[0]] : group.Days.Select(day => Week[day]).ToArray(),
            ["opens"] = group.Open,
            ["closes"] = group.Close
        }).ToArray();
    }

    private static bool IsClosed(string line)
    {
        var folded = Fold(line);
        return folded.Contains("kapali", StringComparison.Ordinal) || folded.Contains("closed", StringComparison.Ordinal);
    }

    private static (string Open, string Close)? ReadTime(string line)
    {
        var match = TimeRange.Match(line);
        if (!match.Success)
            return null;
        if (!Clock(match.Groups[1].Value, match.Groups[2].Value, out var open))
            return null;
        if (!Clock(match.Groups[3].Value, match.Groups[4].Value, out var close))
            return null;
        return (open, close);
    }

    private static bool Clock(string hourText, string minuteText, out string formatted)
    {
        formatted = "";
        if (!int.TryParse(hourText, NumberStyles.None, CultureInfo.InvariantCulture, out var hour) ||
            !int.TryParse(minuteText, NumberStyles.None, CultureInfo.InvariantCulture, out var minute) ||
            hour is < 0 or > 23 || minute is < 0 or > 59)
            return false;

        formatted = $"{hour:00}:{minute:00}";
        return true;
    }

    private static List<int> DayIndexes(string line)
    {
        var folded = Fold(line);
        var tokens = Word.Matches(folded).Select(match => DayIndex(match.Value)).Where(day => day.HasValue).Select(day => day!.Value).ToList();
        if (tokens.Count == 2 && tokens[1] > tokens[0] && folded.Contains('-'))
            return Enumerable.Range(tokens[0], tokens[1] - tokens[0] + 1).ToList();
        return tokens.Distinct().ToList();
    }

    private static int? DayIndex(string token) => token switch
    {
        "pzt" or "pazartesi" or "monday" or "mon" => 0,
        "sal" or "sali" or "tuesday" or "tue" => 1,
        "car" or "carsamba" or "wednesday" or "wed" => 2,
        "per" or "persembe" or "thursday" or "thu" => 3,
        "cum" or "cuma" or "friday" or "fri" => 4,
        "cmt" or "cumartesi" or "saturday" or "sat" => 5,
        "paz" or "pazar" or "sunday" or "sun" => 6,
        _ => null
    };

    private static string Fold(string value)
    {
        var lower = value.ToLower(CultureInfo.GetCultureInfo("tr-TR"));
        var sb = new StringBuilder(lower.Length);
        foreach (var ch in lower)
        {
            sb.Append(ch switch
            {
                'ı' => 'i',
                'ş' => 's',
                'ğ' => 'g',
                'ü' => 'u',
                'ö' => 'o',
                'ç' => 'c',
                _ => ch
            });
        }
        return sb.ToString();
    }
}
