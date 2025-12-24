using System.ComponentModel.DataAnnotations;

namespace LuxeSalon.Core.Entities;

public class BlogPost
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Content { get; set; } = string.Empty; // Rich text
    
    public string? Summary { get; set; }
    
    public string? HeaderImageUrl { get; set; }
    
    public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
    
    public string Author { get; set; } = "Luxe Salon Team";
    
    public bool IsPublished { get; set; } = true;
}
