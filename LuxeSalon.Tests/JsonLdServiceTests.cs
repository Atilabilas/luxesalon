using LuxeSalon.Web.Services;
using System.Text.Json;
using Xunit;

namespace LuxeSalon.Tests;

public class JsonLdServiceTests
{
    private static readonly Dictionary<string, string> Settings = new()
    {
        ["Site_Url"] = "https://celilatilla.com",
        ["Site_Name"] = "Celil & Atilla",
        ["SEO_Description"] = "Test",
        ["Contact_Address"] = "Kazımdirik Mah. Gediz Cd. 21 A",
        ["Map_Lat"] = "38.46234930909974",
        ["Map_Lng"] = "27.208800699999998",
        ["Map_Google_Url"] = "https://maps.google.com",
        ["Social_Instagram"] = "https://www.instagram.com/celil_atilla/"
    };

    [Fact]
    public void HairSalon_IncludesGeoAndAddress()
    {
        var json = JsonLdService.HairSalon(Settings);
        using var doc = JsonDocument.Parse(json);
        Assert.Equal("https://celilatilla.com/#salon", doc.RootElement.GetProperty("@id").GetString());
        Assert.Equal("Bornova", doc.RootElement.GetProperty("address").GetProperty("addressLocality").GetString());
        Assert.Contains("HairSalon", json);
        Assert.Contains("/images/logo.png", json);
    }

    [Fact]
    public void BlogPosting_UsesOrganizationAuthorAndSeparateDates()
    {
        var json = JsonLdService.BlogPosting(
            "Title",
            "Summary",
            "/blog/test",
            "/images/services/balyaj.jpg",
            new DateTime(2026, 9, 1),
            new DateTime(2026, 9, 8),
            "Celil & Atilla",
            Settings);
        using var doc = JsonDocument.Parse(json);
        Assert.Equal("Organization", doc.RootElement.GetProperty("author").GetProperty("@type").GetString());
        Assert.Equal("2026-09-01", doc.RootElement.GetProperty("datePublished").GetString());
        Assert.Equal("2026-09-08", doc.RootElement.GetProperty("dateModified").GetString());
    }

    [Fact]
    public void Breadcrumb_EnglishPageLinksToEnglishHome()
    {
        using var doc = JsonDocument.Parse(JsonLdService.Breadcrumb("Services", "/en/services", Settings));
        var home = doc.RootElement.GetProperty("itemListElement")[0];
        Assert.Equal("Home", home.GetProperty("name").GetString());
        Assert.Equal("https://celilatilla.com/en", home.GetProperty("item").GetString());
    }

    [Fact]
    public void HairSalon_UsesVisiblePhoneAndHours()
    {
        var settings = new Dictionary<string, string>(Settings)
        {
            ["Contact_Phone"] = "0555 000 00 00",
            ["Contact_Hours"] = "09:00-18:00\nPazar: Kapalı"
        };

        using var doc = JsonDocument.Parse(JsonLdService.HairSalon(settings));
        Assert.Equal("0555 000 00 00", doc.RootElement.GetProperty("telephone").GetString());
        var hours = doc.RootElement.GetProperty("openingHoursSpecification");
        Assert.Equal(1, hours.GetArrayLength());
        Assert.Equal("09:00", hours[0].GetProperty("opens").GetString());
        Assert.Equal("18:00", hours[0].GetProperty("closes").GetString());
        Assert.DoesNotContain("Sunday", hours[0].GetProperty("dayOfWeek").ToString());
        Assert.Contains("Monday", hours[0].GetProperty("dayOfWeek").ToString());
    }

    [Fact]
    public void HairSalon_KeepsCurrentHourSplit()
    {
        var settings = new Dictionary<string, string>(Settings)
        {
            ["Contact_Phone"] = "0534 794 43 93",
            ["Contact_Hours"] = "Pzt - Cmt: 08:30 - 20:00\nPazar: 10:00 - 19:00"
        };

        using var doc = JsonDocument.Parse(JsonLdService.HairSalon(settings));
        var hours = doc.RootElement.GetProperty("openingHoursSpecification");
        Assert.Equal("08:30", hours[0].GetProperty("opens").GetString());
        Assert.Equal("20:00", hours[0].GetProperty("closes").GetString());
        Assert.Equal("10:00", hours[1].GetProperty("opens").GetString());
        Assert.Equal("Sunday", hours[1].GetProperty("dayOfWeek").GetString());
    }

    [Theory]
    [InlineData("")]
    [InlineData("abc")]
    [InlineData("38,46234930909974")]
    public void HairSalon_OmitsGeoWhenCoordinateIsUnusable(string latitude)
    {
        var settings = new Dictionary<string, string>(Settings) { ["Map_Lat"] = latitude };

        var json = JsonLdService.HairSalon(settings);

        using var doc = JsonDocument.Parse(json);
        Assert.False(doc.RootElement.TryGetProperty("geo", out _));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void BlogPosting_OmitsImageWhenCoverIsMissing(string? imageUrl)
    {
        var json = JsonLdService.BlogPosting(
            "Title", "Summary", "/blog/test", imageUrl,
            new DateTime(2026, 9, 1), new DateTime(2026, 9, 8),
            "Celil & Atilla", Settings);

        using var doc = JsonDocument.Parse(json);
        Assert.False(doc.RootElement.TryGetProperty("image", out _));
    }

    [Fact]
    public void BlogPosting_OrganizationAuthorDoesNotDependOnSiteTitle()
    {
        var settings = new Dictionary<string, string>(Settings) { ["Site_Name"] = "Yeni Salon Adı" };
        var json = JsonLdService.BlogPosting(
            "Title", "Summary", "/blog/test", "/images/services/balyaj.jpg",
            new DateTime(2026, 9, 1), new DateTime(2026, 9, 8),
            "Celil & Atilla", settings);

        using var doc = JsonDocument.Parse(json);
        Assert.Equal("Organization", doc.RootElement.GetProperty("author").GetProperty("@type").GetString());
        Assert.Equal("Celil & Atilla", doc.RootElement.GetProperty("author").GetProperty("name").GetString());
    }
}
