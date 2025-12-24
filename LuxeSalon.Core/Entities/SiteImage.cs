using System.ComponentModel.DataAnnotations;

namespace LuxeSalon.Core.Entities;

public class SiteImage
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Section { get; set; } = string.Empty; // Hero, About, Loader1, Loader2, Service1, etc.
    
    [Required]
    [MaxLength(500)]
    public string ImageUrl { get; set; } = string.Empty;
    
    [MaxLength(200)]
    public string? Title { get; set; }
    
    [MaxLength(500)]
    public string? Description { get; set; }
    
    [MaxLength(100)]
    public string? AltText { get; set; }
    
    public int DisplayOrder { get; set; } = 0;
    
    public bool IsActive { get; set; } = true;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
