using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LuxeSalon.Web.Pages.Admin.Appointments;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Appointment> Appointments { get; set; } = new();

    public static readonly string[] StatusOptions = ["Pending", "Confirmed", "Cancelled"];

    public async Task OnGetAsync()
    {
        Appointments = await _context.Appointments
            .Include(a => a.Service)
            .OrderByDescending(a => a.AppointmentDate)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostUpdateStatusAsync(int id, string status)
    {
        if (!StatusOptions.Contains(status))
            return BadRequest();

        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null) return NotFound();

        appointment.Status = status;
        await _context.SaveChangesAsync();
        return RedirectToPage();
    }
}
