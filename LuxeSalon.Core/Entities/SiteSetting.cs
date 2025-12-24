using System.ComponentModel.DataAnnotations;

namespace LuxeSalon.Core.Entities;

public class SiteSetting
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Key { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;

    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;

    public string Group { get; set; } = "General"; // General, Contact, Social, SEO

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
