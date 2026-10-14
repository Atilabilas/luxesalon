using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LuxeSalon.Web.Pages;

public class RandevuModel : PageContentModel
{
    public RandevuModel(ApplicationDbContext context, ContentService content, SiteSettingsService settings)
        : base(context, content, settings) { }

    public SiteImage? HeroImage { get; set; }
    public List<Service> Services { get; set; } = new();
    public string? SuccessMessage { get; set; }
    public int? SelectedServiceId { get; set; }

    public async Task OnGetAsync(int? serviceId = null)
    {
        await LoadContentAsync("Randevu");

        HeroImage = await Cms.GetImageAsync("Hero_Randevu");
        Services = await Context.Services.Where(s => s.IsPublished).OrderBy(s => s.Name).ToListAsync();
        SelectedServiceId = Services.Any(s => s.Id == serviceId) ? serviceId : null;

        SetSeo(
            GetContent("randevu_page_title", CurrentCulture == "en" ? "Appointment | Celil & Atilla" : "Randevu | Celil & Atilla"),
            CurrentCulture == "en"
                ? "Book a haircut, colour or care appointment at Celil & Atilla in Kazımdirik, Bornova. Call or use the form."
                : "Celil & Atilla Kazımdirik Bornova’da kesim, renk veya bakım randevusu. Formu doldurun veya 0534 794 43 93’ü arayın.",
            SitePaths.Appointment(CurrentCulture),
            HeroImage?.ImageUrl,
            "website",
            null,
            SitePaths.Appointment("tr"),
            SitePaths.Appointment("en"));

        if (TempData["Success"] != null)
            SuccessMessage = TempData["Success"]?.ToString();
    }

    public async Task<IActionResult> OnPostAsync(
        string FirstName, string LastName, string Phone, string? Email,
        DateTime Date, string Time, int ServiceId, string? Notes)
    {
        await LoadContentAsync("Randevu");
        HeroImage = await Cms.GetImageAsync("Hero_Randevu");
        Services = await Context.Services.Where(s => s.IsPublished).OrderBy(s => s.Name).ToListAsync();

        var service = await Context.Services.FindAsync(ServiceId);
        if (service == null)
            return Page();

        var timeSpan = TimeSpan.Parse(Time);
        var appointmentDateTime = Date.Add(timeSpan);

        var appointment = new Appointment
        {
            CustomerName = $"{FirstName} {LastName}",
            CustomerEmail = Email ?? "",
            CustomerPhone = Phone,
            AppointmentDate = appointmentDateTime,
            ServiceId = ServiceId,
            Notes = Notes,
            Status = "Beklemede"
        };

        Context.Appointments.Add(appointment);
        await Context.SaveChangesAsync();

        var message = WhatsAppLink.AppointmentMessage(
            CurrentCulture,
            appointment.CustomerName,
            Phone,
            Email,
            appointmentDateTime,
            service.LocalizedName(CurrentCulture),
            Notes);

        return Redirect(WhatsAppLink.Chat(Setting("Whatsapp_Number"), message));
    }

    public string FullAddress() => $"{Setting("Contact_Address")}<br/>{Setting("Contact_City")}";
    public string ContactHours() => Setting("Contact_Hours").Replace("\n", "<br/>");
}
