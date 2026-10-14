using LuxeSalon.Core.Utilities;
using Xunit;

namespace LuxeSalon.Tests;

public class SlugHelperTests
{
    [Fact]
    public void FromTitle_MapsTurkishCharacters()
    {
        Assert.Equal("istanbul", SlugHelper.FromTitle("İstanbul"));
        Assert.Equal("isik", SlugHelper.FromTitle("ışık"));
        Assert.Equal("balyaj-ombre", SlugHelper.FromTitle("Balyaj, Ombre"));
        Assert.Equal("sac-botoksu", SlugHelper.FromTitle("Saç Botoksu"));
    }

    [Fact]
    public void EnsureUnique_AppendsNumericSuffix()
    {
        var slug = SlugHelper.EnsureUnique("balyaj", new[] { "balyaj", "balyaj-2" });
        Assert.Equal("balyaj-3", slug);
    }

    [Fact]
    public void SitePaths_CulturePrefix()
    {
        Assert.Equal("/en/blog", LuxeSalon.Web.Services.SitePaths.Blog("en"));
        Assert.Equal("/blog", LuxeSalon.Web.Services.SitePaths.Blog("tr"));
        Assert.Equal("/hizmetler/balyaj-ombre-sombre", LuxeSalon.Web.Services.SitePaths.Service("tr", "balyaj-ombre-sombre"));
        Assert.Equal("/en/services/balayage-ombre-sombre", LuxeSalon.Web.Services.SitePaths.Service("en", "balayage-ombre-sombre"));
    }
}
