using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LuxeSalon.Infrastructure.Data;

namespace LuxeSalon.Web.Pages.Admin;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public int SiteImageCount { get; set; }
    public int TeamMemberCount { get; set; }
    public int PriceItemCount { get; set; }
    public int AppointmentCount { get; set; }
    public int BlogPostCount { get; set; }
    public int ProductCount { get; set; }
    public int ServiceCount { get; set; }
    public int TestimonialCount { get; set; }
    public int GalleryCount { get; set; }

    public async Task OnGetAsync()
    {
        SiteImageCount = await _context.SiteImages.CountAsync();
        TeamMemberCount = await _context.TeamMembers.CountAsync();
        PriceItemCount = await _context.PriceItems.CountAsync();
        AppointmentCount = await _context.Appointments.CountAsync();
        BlogPostCount = await _context.BlogPosts.CountAsync();
        ProductCount = await _context.Products.CountAsync();
        ServiceCount = await _context.Services.CountAsync();
        TestimonialCount = await _context.Testimonials.CountAsync();
        GalleryCount = await _context.GalleryItems.CountAsync();
    }
}
