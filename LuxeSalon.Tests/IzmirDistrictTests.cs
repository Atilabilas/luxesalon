using LuxeSalon.Web.Services;
using Xunit;

namespace LuxeSalon.Tests;

public class IzmirDistrictTests
{
    [Fact]
    public void DistrictList_CoversThirtyIzmirDistricts_AndOnlyBornovaHasTheSalon()
    {
        var names = IzmirDistricts.All;
        Assert.Equal(30, names.Distinct(StringComparer.Ordinal).Count());
        Assert.Contains("Bornova", names);
        Assert.Contains("Karşıyaka", names);
        Assert.Contains("Çeşme", names);
        Assert.Single(IzmirDistricts.Groups, group => group.ContainsSalon);
        Assert.Equal("Bornova", Assert.Single(IzmirDistricts.Groups.Single(group => group.ContainsSalon).Names));
        Assert.DoesNotContain(names, name => name.Contains("kadın kuaför", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void HomeCopy_UsesEachHeadQueryOnce()
    {
        Assert.Contains("Bornova Kadın Kuaförü", LocalHeadTerms.HomeTitleTr);
        Assert.InRange(LocalHeadTerms.HomeTitleTr.Length, 1, 60);
        Assert.Contains("Bornova kadın kuaförü", LocalHeadTerms.HomeDescriptionTr);
        Assert.InRange(LocalHeadTerms.HomeDescriptionTr.Length, 150, 160);
        Assert.InRange(LocalHeadTerms.HomeDescriptionEn.Length, 150, 160);
        Assert.Contains("Bornova kuaför", LocalHeadTerms.HomeSupportTr);
        Assert.Contains("İzmir kadın kuaförü", LocalHeadTerms.HomeSupportTr);
        Assert.DoesNotContain("İzmir kadın kuaförü", LocalHeadTerms.HomeDescriptionTr);
        Assert.Equal("Bornova kadın kuaförü", LocalHeadTerms.HomeFocusTr);
    }

    [Fact]
    public void ServiceSnippets_StayInsideSearchResultLimits()
    {
        Assert.Equal(8, ServiceSearchCopy.Slugs.Length);
        Assert.InRange(ServiceSearchCopy.DirectoryTitleTr.Length, 1, 60);
        Assert.InRange(ServiceSearchCopy.DirectoryMetaTr.Length, 150, 160);
        Assert.InRange(ServiceSearchCopy.DirectoryMetaEn.Length, 150, 160);
        foreach (var slug in ServiceSearchCopy.Slugs)
        {
            Assert.True(ServiceSearchCopy.TryGet(slug, out var snippet), slug);
            Assert.InRange(snippet.TitleTr.Length, 1, 60);
            Assert.InRange(snippet.TitleEn.Length, 1, 60);
            Assert.StartsWith("Bornova", snippet.TitleTr);
            Assert.StartsWith("Bornova", snippet.TitleEn);
            Assert.Equal(ServiceSearchCopy.Query(slug, "tr"), snippet.TitleTr.Split('|')[0].Trim());
            Assert.InRange(snippet.MetaTr.Length, 150, 160);
            Assert.InRange(snippet.MetaEn.Length, 150, 160);
            Assert.Contains("Bornova", snippet.MetaTr);
            Assert.Contains("0534 794 43 93", snippet.MetaTr);
            Assert.DoesNotContain("en iyi", snippet.TitleTr, StringComparison.OrdinalIgnoreCase);
            var rewritten = ServiceSearchCopy.WithQueryHeadings("<h2>Ne için?</h2><h2>Nasıl ilerler?</h2>", slug, "tr");
            Assert.Contains($"<h2>{ServiceSearchCopy.Query(slug, "tr")}: ne için?</h2>", rewritten);
            Assert.DoesNotContain("<h2>Ne için?</h2>", rewritten);
        }
    }

    [Theory]
    [InlineData("Keratin ve Saç Botoksu | Celil & Atilla", "Keratin ve Saç Botoksu | Celil & Atilla Bornova")]
    [InlineData("Kadın Saç Kesimi | Celil & Atilla Bornova", "Kadın Saç Kesimi | Celil & Atilla Bornova")]
    public void ServiceTitle_KeepsASingleBornovaMention(string input, string expected)
    {
        Assert.Equal(expected, LocalHeadTerms.WithBornova(input));
    }
}
