using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Core.Entities;

namespace LuxeSalon.Web.Pages;

public class RandevuModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public RandevuModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public string? SuccessMessage { get; set; }

    public void OnGet()
    {
        if (TempData["Success"] != null)
        {
            SuccessMessage = TempData["Success"]?.ToString();
        }
    }

    public async Task<IActionResult> OnPostAsync(string FirstName, string LastName, string Phone, string? Email, 
        DateTime Date, string Time, string Service, string? Notes)
    {
        // Combine date and time
        var timeSpan = TimeSpan.Parse(Time);
        var appointmentDateTime = Date.Add(timeSpan);

        var appointment = new Appointment
        {
            CustomerName = $"{FirstName} {LastName}",
            CustomerEmail = Email ?? "",
            CustomerPhone = Phone,
            AppointmentDate = appointmentDateTime,
            ServiceId = 1, // Default service, can be updated later
            Notes = $"Hizmet: {Service}. {Notes}",
            Status = "Beklemede"
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Randevunuz başarıyla oluşturuldu! En kısa sürede size dönüş yapacağız.";
        return RedirectToPage();
    }
}
