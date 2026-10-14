using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Web.Services;
using Microsoft.EntityFrameworkCore;

namespace LuxeSalon.Web.Pages;

public class IzmirIlceleriModel : PageContentModel
{
    public IzmirIlceleriModel(ApplicationDbContext context, ContentService content, SiteSettingsService settings)
        : base(context, content, settings) { }

    public List<Service> Services { get; set; } = new();

    public async Task OnGetAsync()
    {
        await LoadContentAsync();
        Services = await Context.Services.AsNoTracking()
            .Where(service => service.IsPublished)
            .OrderByDescending(service => service.IsLandingPage)
            .ThenBy(service => service.Name)
            .ToListAsync();
    }
}
