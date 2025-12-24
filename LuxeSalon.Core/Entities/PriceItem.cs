using System.ComponentModel.DataAnnotations;

namespace LuxeSalon.Core.Entities;

public class PriceItem
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Category { get; set; } = string.Empty; // Saç Hizmetleri, Renk Hizmetleri, Bakım
    
    [Required]
    [MaxLength(150)]
    public string ServiceName { get; set; } = string.Empty;
    
    [Required]
    public decimal Price { get; set; }
    
    [MaxLength(300)]
    public string? Description { get; set; }
    
    public int DisplayOrder { get; set; } = 0;
    
    public bool IsActive { get; set; } = true;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
