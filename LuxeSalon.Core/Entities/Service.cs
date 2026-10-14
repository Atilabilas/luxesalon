using System.ComponentModel.DataAnnotations;

namespace LuxeSalon.Core.Entities;

public class Service
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? NameEn { get; set; }

    [MaxLength(200)]
    public string Slug { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? SlugEn { get; set; }

    public string Description { get; set; } = string.Empty;

    public string? DescriptionEn { get; set; }

    public string? LongDescription { get; set; }

    public string? LongDescriptionEn { get; set; }

    [MaxLength(200)]
    public string? SeoTitle { get; set; }

    [MaxLength(200)]
    public string? SeoTitleEn { get; set; }

    [MaxLength(320)]
    public string? MetaDescription { get; set; }

    [MaxLength(320)]
    public string? MetaDescriptionEn { get; set; }

    [MaxLength(200)]
    public string? Tags { get; set; }

    [Range(0, 1000000)]
    public decimal Price { get; set; }

    public int DurationMinutes { get; set; }

    public string Category { get; set; } = "General";

    public string ImageUrl { get; set; } = string.Empty;

    public bool IsLandingPage { get; set; }

    public bool IsPublished { get; set; } = true;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public string LocalizedName(string culture) =>
        culture.StartsWith("en", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(NameEn)
            ? NameEn!
            : Name;

    public string LocalizedDescription(string culture) =>
        culture.StartsWith("en", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(DescriptionEn)
            ? DescriptionEn!
            : Description;

    public string LocalizedLongDescription(string culture) =>
        culture.StartsWith("en", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(LongDescriptionEn)
            ? LongDescriptionEn!
            : LongDescription ?? Description;

    public string LocalizedSlug(string culture) =>
        culture.StartsWith("en", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(SlugEn)
            ? SlugEn!
            : Slug;

    public string LocalizedSeoTitle(string culture) =>
        culture.StartsWith("en", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(SeoTitleEn)
            ? SeoTitleEn!
            : SeoTitle ?? Name;

    public string LocalizedMetaDescription(string culture) =>
        culture.StartsWith("en", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(MetaDescriptionEn)
            ? MetaDescriptionEn!
            : MetaDescription ?? Description;

    public bool HasEnglish =>
        !string.IsNullOrWhiteSpace(NameEn) &&
        !string.IsNullOrWhiteSpace(SlugEn) &&
        !string.IsNullOrWhiteSpace(LongDescriptionEn);
}
