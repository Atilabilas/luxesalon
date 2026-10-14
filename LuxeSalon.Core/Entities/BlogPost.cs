using System.ComponentModel.DataAnnotations;

namespace LuxeSalon.Core.Entities;

public class BlogPost
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? TitleEn { get; set; }

    [Required]
    [MaxLength(200)]
    public string Slug { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? SlugEn { get; set; }

    [Required]
    public string Content { get; set; } = string.Empty;

    public string? ContentEn { get; set; }

    public string? Summary { get; set; }

    public string? SummaryEn { get; set; }

    public string? HeaderImageUrl { get; set; }

    [MaxLength(200)]
    public string? Tags { get; set; }

    public DateTime PublishedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public string Author { get; set; } = "Celil & Atilla";

    public bool IsPublished { get; set; } = true;

    public string LocalizedTitle(string culture) =>
        culture.StartsWith("en", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(TitleEn)
            ? TitleEn!
            : Title;

    public string LocalizedSummary(string culture) =>
        culture.StartsWith("en", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(SummaryEn)
            ? SummaryEn!
            : Summary ?? "";

    public string LocalizedContent(string culture) =>
        culture.StartsWith("en", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(ContentEn)
            ? ContentEn!
            : Content;

    public string LocalizedSlug(string culture) =>
        culture.StartsWith("en", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(SlugEn)
            ? SlugEn!
            : (string.IsNullOrWhiteSpace(Slug) ? Id.ToString() : Slug);

    public bool HasEnglish =>
        !string.IsNullOrWhiteSpace(TitleEn) &&
        !string.IsNullOrWhiteSpace(SlugEn) &&
        !string.IsNullOrWhiteSpace(ContentEn);
}
