using LuxeSalon.Web.Services;
using Xunit;

namespace LuxeSalon.Tests;

public class GooglebotAccessTests
{
    static string RepoFile(string relativePath)
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null && !File.Exists(Path.Combine(dir.FullName, "LuxeSalon.sln")))
            dir = dir.Parent;
        Assert.NotNull(dir);
        return File.ReadAllText(Path.Combine(dir!.FullName, relativePath.Replace('/', Path.DirectorySeparatorChar)));
    }

    [Fact]
    public void Robots_LetsGooglebotIntoEveryPublicUrl()
    {
        var robots = RepoFile("LuxeSalon.Web/wwwroot/robots.txt");
        Assert.Contains("User-agent: *", robots);
        Assert.Contains("Allow: /", robots);
        Assert.Contains("Disallow: /Admin", robots);
        Assert.Contains("Sitemap: https://celilatilla.com/sitemap.xml", robots);
        Assert.DoesNotContain("Googlebot", robots, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Disallow: /hizmetler", robots);
        Assert.DoesNotContain("Disallow: /blog", robots);
        Assert.DoesNotContain("Disallow: /izmir", robots);
    }

    [Fact]
    public void PublicPages_AreIndexable_AndDoNotCloakTheBot()
    {
        var layout = RepoFile("LuxeSalon.Web/Pages/Shared/_Layout.cshtml");
        Assert.Contains("index, follow, max-image-preview:large", layout);
        Assert.DoesNotContain("User-Agent", layout, StringComparison.OrdinalIgnoreCase);

        foreach (var page in new[]
        {
            "Pages/Index.cshtml",
            "Pages/Hizmetler.cshtml",
            "Pages/Hizmetler/Details.cshtml",
            "Pages/IzmirIlceleri.cshtml",
            "Pages/Blog/Index.cshtml",
            "Pages/Blog/Details.cshtml",
            "Pages/Hakkimizda.cshtml",
            "Pages/Randevu.cshtml"
        })
        {
            var source = RepoFile("LuxeSalon.Web/" + page);
            Assert.DoesNotContain("noindex", source, StringComparison.OrdinalIgnoreCase);
        }
    }

    [Fact]
    public void VisibleHeadings_MatchTheClaims()
    {
        var home = RepoFile("LuxeSalon.Web/Pages/Index.cshtml");
        Assert.Contains("LocalHeadTerms.HomeFocusTr", home);
        Assert.Equal("Bornova kadın kuaförü", LocalHeadTerms.HomeFocusTr);
        Assert.Contains("Bornova kuaför", LocalHeadTerms.HomeSupportTr);
        Assert.Contains("İzmir kadın kuaförü", LocalHeadTerms.HomeSupportTr);
        Assert.Contains("Gediz Cd. 21 A", ServiceSearchCopy.ServiceLeadTr);
        Assert.Contains("0534 794 43 93", ServiceSearchCopy.DirectoryMetaTr);

        foreach (var slug in ServiceSearchCopy.Slugs)
        {
            var query = ServiceSearchCopy.Query(slug, "tr");
            Assert.StartsWith("Bornova", query);
            Assert.StartsWith(query, ServiceSearchCopy.TryGet(slug, out var snippet) ? snippet.TitleTr : "");
            var body = ServiceSearchCopy.WithQueryHeadings("<h2>Ne için?</h2>", slug, "tr");
            Assert.Contains($"<h2>{query}: ne için?</h2>", body);
        }
    }
}
