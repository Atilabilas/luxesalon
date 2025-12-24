using System.ComponentModel.DataAnnotations;

namespace LuxeSalon.Core.Entities;

public class Appointment
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string CustomerName { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string CustomerEmail { get; set; } = string.Empty;
    
    [Required]
    [Phone]
    public string CustomerPhone { get; set; } = string.Empty;
    
    public int ServiceId { get; set; }
    public Service? Service { get; set; }
    
    [Required]
    public DateTime AppointmentDate { get; set; }
    
    public string Status { get; set; } = "Pending"; // Pending, Confirmed, Cancelled
    
    public string? Notes { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
