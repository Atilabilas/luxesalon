using System.ComponentModel.DataAnnotations;

namespace LuxeSalon.Core.Entities;

public class Product
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    [Range(0, 1000000)]
    public decimal Price { get; set; }
    
    public int StockQuantity { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public string Category { get; set; } = "Hair Care";
}
