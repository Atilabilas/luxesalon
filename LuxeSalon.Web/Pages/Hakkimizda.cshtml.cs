using LuxeSalon.Core.Entities;

using LuxeSalon.Infrastructure.Data;

using LuxeSalon.Web.Services;

using Microsoft.EntityFrameworkCore;



namespace LuxeSalon.Web.Pages;



public class HakkimizdaModel : PageContentModel

{

    public HakkimizdaModel(ApplicationDbContext context, ContentService content, SiteSettingsService settings)

        : base(context, content, settings) { }



    public SiteImage? HeroImage { get; set; }

    public SiteImage? AboutImage { get; set; }

    public SiteImage? CtaImage { get; set; }

    public List<TeamMember> TeamMembers { get; set; } = new();

    public List<StatItem> StatItems { get; set; } = new();

    public List<AwardItem> AwardItems { get; set; } = new();



    public async Task OnGetAsync()
    {
        await LoadContentAsync("Hakkimizda");

        var images = await Context.SiteImages.Where(i => i.IsActive).ToListAsync();
        HeroImage = images.FirstOrDefault(i => i.Section == "Hero_Hakkimizda");
        AboutImage = images.FirstOrDefault(i => i.Section == "About_Hakkimizda");
        CtaImage = images.FirstOrDefault(i => i.Section == "Cta_Hakkimizda");

        TeamMembers = await Context.TeamMembers.Where(t => t.IsActive).OrderBy(t => t.DisplayOrder).ToListAsync();
        StatItems = await Context.StatItems.Where(s => s.Section == "Hakkimizda").OrderBy(s => s.DisplayOrder).ToListAsync();
        AwardItems = await Context.AwardItems.Where(a => a.IsActive).OrderBy(a => a.DisplayOrder).ToListAsync();

        SetSeo(
            GetContent("about_page_title", CurrentCulture == "en" ? "About | Celil & Atilla" : "Hakkımızda | Celil & Atilla"),
            GetContent("about_description1", Setting("SEO_Description")),
            SitePaths.About(CurrentCulture),
            HeroImage?.ImageUrl,
            "website",
            null,
            SitePaths.About("tr"),
            SitePaths.About("en"));
    }

}


