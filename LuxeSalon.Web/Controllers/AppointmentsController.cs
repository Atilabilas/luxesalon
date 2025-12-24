using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LuxeSalon.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AppointmentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("check-availability")]
    public async Task<IActionResult> CheckAvailability([FromBody] CheckAvailabilityRequest request)
    {
        if (request.Date < DateTime.Today)
        {
            return BadRequest(new { message = "Geçmiş bir tarih seçemezsiniz." });
        }

        // Check if slot is taken
        // Assuming slots are 1 hour for simplicity or strictly defined
        // In real app, we check Service Duration. Here for "Quick Appointment", we might assume standard slot.
        // Let's assume 1 hour slots.
        
        var appointmentDate = request.Date.Date.Add(request.Time);
        
        var isTaken = await _context.Appointments
            .AnyAsync(a => a.AppointmentDate == appointmentDate && a.Status != "Cancelled");

        if (isTaken)
        {
            return BadRequest(new { message = "Seçilen saat doludur. Lütfen başka bir saat seçiniz." });
        }

        return Ok(new { success = true });
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var appointmentDate = request.Date.Date.Add(request.Time);

        // Double check availability
         var isTaken = await _context.Appointments
            .AnyAsync(a => a.AppointmentDate == appointmentDate && a.Status != "Cancelled");

        if (isTaken)
        {
             return BadRequest(new { message = "Maalesef işlem sırasında bu saat doldu." });
        }

        var appointment = new Appointment
        {
            CustomerName = request.Name,
            CustomerEmail = request.Email,
            CustomerPhone = request.Phone,
            AppointmentDate = appointmentDate,
            ServiceId = request.ServiceId > 0 ? request.ServiceId : 1, // Default to first service if not selected? Or required?
            // For now, let's assume a default service or let them choose in the modal if we add that field.
            // The prompt said "Date, Time, Email" in the bar. The modal adds "Name, Phone". Service is missing?
            // I will default to a "General Consultation" or similar if no service selected, OR add Service to Modal.
            // Let's add ServiceId to the request but optional/default.
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };

        // If ServiceId is 0/null, try get the first service
        if(appointment.ServiceId <= 0) {
             var firstService = await _context.Services.FirstOrDefaultAsync();
             if(firstService != null) appointment.ServiceId = firstService.Id;
        }

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return Ok(new { success = true, message = "Randevunuz başarıyla oluşturuldu." });
    }
}

public class CheckAvailabilityRequest
{
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
}

public class CreateAppointmentRequest
{
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public int ServiceId { get; set; }
}
