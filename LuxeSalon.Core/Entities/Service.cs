using System.ComponentModel.DataAnnotations;

namespace LuxeSalon.Core.Entities;

public class Service
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    [Range(0, 1000000)]
    public decimal Price { get; set; }
    
    public int DurationMinutes { get; set; }
    
    public string Category { get; set; } = "General";
    
    public string ImageUrl { get; set; } = string.Empty;
}
