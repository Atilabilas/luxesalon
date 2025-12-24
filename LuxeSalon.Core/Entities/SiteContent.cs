namespace LuxeSalon.Core.Entities;

/// <summary>
/// Entity for storing all editable site content blocks.
/// Each content block has a unique key and can have multilingual values.
/// </summary>
public class SiteContent
{
    public int Id { get; set; }
    
    /// <summary>
    /// Unique identifier for the content block (e.g., "hero_title", "about_description")
    /// </summary>
    public string Key { get; set; } = string.Empty;
    
    /// <summary>
    /// Section/Category of the content (e.g., "Hero", "About", "Services", "Pricing", "Contact")
    /// </summary>
    public string Section { get; set; } = string.Empty;
    
    /// <summary>
    /// The actual content value (HTML allowed for rich content)
    /// </summary>
    public string Value { get; set; } = string.Empty;
    
    /// <summary>
    /// Content type: text, html, image_url
    /// </summary>
    public string ContentType { get; set; } = "text";
    
    /// <summary>
    /// Display order within section
    /// </summary>
    public int DisplayOrder { get; set; }
    
    /// <summary>
    /// Last updated timestamp
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
