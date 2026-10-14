using System.ComponentModel.DataAnnotations;

namespace LuxeSalon.Core.Entities;

public class AwardItem
{
    public int Id { get; set; }

    [MaxLength(20)]
    public string Icon { get; set; } = "🏆";

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(200)]
    public string Subtitle { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? Year { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
