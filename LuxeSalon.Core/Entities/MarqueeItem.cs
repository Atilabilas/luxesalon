using System.ComponentModel.DataAnnotations;

namespace LuxeSalon.Core.Entities;

public class MarqueeItem
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Text { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
