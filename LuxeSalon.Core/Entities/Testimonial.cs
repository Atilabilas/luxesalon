using System.ComponentModel.DataAnnotations;

namespace LuxeSalon.Core.Entities;

public class Testimonial
{
    public int Id { get; set; }

    [Required]
    public string Quote { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string AuthorName { get; set; } = string.Empty;

    [MaxLength(200)]
    public string AuthorTitle { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? ImageUrl { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
