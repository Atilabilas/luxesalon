using System.ComponentModel.DataAnnotations;

namespace LuxeSalon.Core.Entities;

/// <summary>
/// Editable site content block. Culture defaults to Turkish (tr).
/// </summary>
public class SiteContent
{
    public int Id { get; set; }

    public string Key { get; set; } = string.Empty;

    public string Section { get; set; } = string.Empty;

    /// <summary>
    /// Language code: tr | en
    /// </summary>
    [MaxLength(10)]
    public string Culture { get; set; } = "tr";

    public string Value { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Label { get; set; }

    public string ContentType { get; set; } = "text";

    public int DisplayOrder { get; set; }

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
