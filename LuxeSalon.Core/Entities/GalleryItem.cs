using System.ComponentModel.DataAnnotations;

namespace LuxeSalon.Core.Entities;

public class GalleryItem
{
    public int Id { get; set; }

    /// <summary>Portfolio or Instagram</summary>
    [Required]
    [MaxLength(50)]
    public string Section { get; set; } = "Portfolio";

    [Required]
    [MaxLength(500)]
    public string ImageUrl { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Title { get; set; }

    [MaxLength(200)]
    public string? OverlayText { get; set; }

    [MaxLength(500)]
    public string? LinkUrl { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
