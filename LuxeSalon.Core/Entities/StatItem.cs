using System.ComponentModel.DataAnnotations;

namespace LuxeSalon.Core.Entities;

public class StatItem
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Value { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Label { get; set; } = string.Empty;

    /// <summary>Hakkimizda, About, etc.</summary>
    [Required]
    [MaxLength(50)]
    public string Section { get; set; } = "Hakkimizda";

    public int DisplayOrder { get; set; }

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
