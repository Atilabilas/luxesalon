using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LuxeSalon.Web.Pages;

/// <summary>
/// Index page model handling the hero section and quick appointment requests.
/// Follows SOLID principles and Clean Architecture by utilizing the Infrastructure layer.
/// </summary>
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public DateTime Date { get; set; }

    [BindProperty]
    public string Time { get; set; } = string.Empty;

    [BindProperty]
    public string Email { get; set; } = string.Empty;

    public string? StatusMessage { get; set; }

    // CMS Data
    public SiteImage? HeroImage { get; set; }
    public SiteImage? AboutImage { get; set; }
    public SiteImage? PricingImage { get; set; }
    public SiteImage? MarqueeImage { get; set; }
    public List<SiteImage> ServiceImages { get; set; } = new();
    public List<SiteImage> LoaderImages { get; set; } = new();
    public List<TeamMember> TeamMembers { get; set; } = new();
    public List<PriceItem> PriceItems { get; set; } = new();
    public List<string> PriceCategories { get; set; } = new();
    
    // Site Content (texts from database)
    public Dictionary<string, SiteContent> Contents { get; set; } = new();
    
    /// <summary>
    /// Helper method to get content value by key with fallback
    /// </summary>
    public string GetContent(string key, string fallback = "")
    {
        return Contents.TryGetValue(key, out var content) ? content.Value : fallback;
    }
    
    /// <summary>
    /// Get content ID for edit link
    /// </summary>
    public int? GetContentId(string key)
    {
        return Contents.TryGetValue(key, out var content) ? content.Id : null;
    }

    public async Task OnGetAsync()
    {
        // Load site images
        var images = await _context.SiteImages.Where(i => i.IsActive).ToListAsync();
        HeroImage = images.FirstOrDefault(i => i.Section == "Hero");
        AboutImage = images.FirstOrDefault(i => i.Section == "About");
        PricingImage = images.FirstOrDefault(i => i.Section == "Pricing");
        MarqueeImage = images.FirstOrDefault(i => i.Section == "Marquee");
        ServiceImages = images.Where(i => i.Section.StartsWith("Service")).OrderBy(i => i.DisplayOrder).ToList();
        LoaderImages = images.Where(i => i.Section.StartsWith("Loader")).OrderBy(i => i.DisplayOrder).ToList();
        
        // Load team members
        TeamMembers = await _context.TeamMembers.Where(t => t.IsActive).OrderBy(t => t.DisplayOrder).ToListAsync();
        
        // Load price items
        PriceItems = await _context.PriceItems.Where(p => p.IsActive).OrderBy(p => p.Category).ThenBy(p => p.DisplayOrder).ToListAsync();
        PriceCategories = PriceItems.Select(p => p.Category).Distinct().ToList();
        
        // Load site contents (texts)
        var contentList = await _context.SiteContents.ToListAsync();
        Contents = contentList.ToDictionary(c => c.Key, c => c);
    }

    /// <summary>
    /// Handles the quick booking form submission from the landing page.
    /// Captures Date, Time, and Email as per GEMINI.md Rule 4.
    /// </summary>
    public async Task<IActionResult> OnPostQuickBookingAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Combine Date and Time strings into a single DateTime object
        if (DateTime.TryParse($"{Date:yyyy-MM-dd} {Time}", out DateTime fullAppointmentDate))
        {
            // Create a new appointment lead
            // Since Name and Phone are required by the entity but not in the quick bar, 
            // we use placeholders for this initial "Quick" request.
            var appointment = new Appointment
            {
                CustomerName = "Quick Lead",
                CustomerPhone = "Pending",
                CustomerEmail = Email,
                AppointmentDate = fullAppointmentDate,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();

            TempData["StatusMessage"] = "Your request has been received. We will contact you shortly.";
            return RedirectToPage();
        }

        ModelState.AddModelError(string.Empty, "Invalid date or time format.");
        return Page();
    }
}
