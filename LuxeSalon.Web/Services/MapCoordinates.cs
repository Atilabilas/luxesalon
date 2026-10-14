using System.Globalization;

namespace LuxeSalon.Web.Services;

public static class MapCoordinates
{
    public static bool TryParse(string? raw, double min, double max, out double value)
    {
        value = 0;
        if (string.IsNullOrWhiteSpace(raw))
            return false;

        var text = raw.Trim();
        if (text.Contains(',') || text.Contains(' '))
            return false;
        if (!double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
            return false;

        return !double.IsNaN(value) && !double.IsInfinity(value) && value >= min && value <= max;
    }

    public static IReadOnlyList<string> Validate(IReadOnlyDictionary<string, string> updates)
    {
        var errors = new List<string>();
        if (updates.TryGetValue("Map_Lat", out var latitude) && !TryParse(latitude, -90, 90, out _))
            errors.Add("Harita enlemi -90 ile 90 arasında, nokta ayraçlı bir sayı olmalı.");
        if (updates.TryGetValue("Map_Lng", out var longitude) && !TryParse(longitude, -180, 180, out _))
            errors.Add("Harita boylamı -180 ile 180 arasında, nokta ayraçlı bir sayı olmalı.");
        return errors;
    }
}
