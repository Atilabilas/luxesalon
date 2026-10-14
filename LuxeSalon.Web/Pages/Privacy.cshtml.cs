using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Web.Services;

namespace LuxeSalon.Web.Pages;

public class PrivacyModel : PageContentModel
{
    public PrivacyModel(ApplicationDbContext context, ContentService content, SiteSettingsService settings)
        : base(context, content, settings) { }

    public async Task OnGetAsync()
    {
        await LoadContentAsync("Privacy");
        var culture = CurrentCulture;
        SetSeo(
            GetContent("privacy_title", culture == "en" ? "Privacy" : "Gizlilik"),
            culture == "en"
                ? "How Celil & Atilla uses appointment and contact details. Kazımdirik Mah. Gediz Cd. 21 A, Bornova."
                : "Celil & Atilla randevu ve iletişim formunda aldığı bilgilerin kullanımı. Kazımdirik Mah. Gediz Cd. 21 A, Bornova.",
            SitePaths.Privacy(culture),
            null,
            "website",
            null,
            SitePaths.Privacy("tr"),
            SitePaths.Privacy("en"));
    }
}
