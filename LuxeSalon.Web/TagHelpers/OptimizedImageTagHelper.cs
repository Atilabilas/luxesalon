using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using LuxeSalon.Web.Services;

namespace LuxeSalon.Web.TagHelpers;

public sealed record ImageAsset(string Src, string Srcset, int Width, int Height);

public sealed class ImageAssets
{
    public IReadOnlyDictionary<string, ImageAsset> Manifest { get; }

    public ImageAssets(IWebHostEnvironment environment)
    {
        var path = Path.Combine(environment.WebRootPath, "images", "image-manifest.json");
        Manifest = File.Exists(path)
            ? JsonSerializer.Deserialize<Dictionary<string, ImageAsset>>(File.ReadAllText(path), new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new()
            : new Dictionary<string, ImageAsset>();
    }
}

[HtmlTargetElement("img")]
public sealed class OptimizedImageTagHelper : TagHelper
{
    private readonly ImageAssets _assets;
    public OptimizedImageTagHelper(ImageAssets assets) => _assets = assets;

    [ViewContext, HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; } = default!;

    // Run after ASP.NET's image version TagHelper.
    public override int Order => 100;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var src = WebUtility.HtmlDecode(output.Attributes["src"]?.Value?.ToString() ?? "");
        if (src.StartsWith("~/")) src = src[1..];
        src = ContentImageCatalog.Resolve(src);
        var alt = ContentImageCatalog.Alt(src, ViewContext?.HttpContext == null ? "tr" : SitePaths.DetectCulture(ViewContext.HttpContext.Request.Path));
        if (alt != null) output.Attributes.SetAttribute("alt", alt);
        var path = src.Split('?')[0];
        var query = src.Contains('?') ? src[src.IndexOf('?')..] : "";
        if (_assets.Manifest.TryGetValue(path, out var asset))
        {
            output.Attributes.SetAttribute("src", asset.Src + query);
            output.Attributes.SetAttribute("srcset", string.Join(", ", asset.Srcset.Split(", ").Select(s =>
            {
                var parts = s.Split(' ');
                return parts[0] + query + " " + parts[1];
            })));
            SetDefault(output, "sizes", "(max-width: 767px) 100vw, (max-width: 1199px) 50vw, 800px");
            SetDefault(output, "width", asset.Width);
            SetDefault(output, "height", asset.Height);
        }
        else if (Uri.TryCreate(src, UriKind.Absolute, out var uri) && uri.Host.Equals("images.unsplash.com", StringComparison.OrdinalIgnoreCase))
        {
            var parameters = QueryHelpers.ParseQuery(uri.Query).ToDictionary(p => p.Key, p => (string?)p.Value.ToString());
            parameters.Remove("auto");
            parameters["fm"] = "webp";
            parameters["q"] = "80";
            var width = int.TryParse(parameters.GetValueOrDefault("w"), out var requested) ? Math.Clamp(requested, 320, 1200) : 1200;
            parameters["w"] = width.ToString();
            var origin = uri.GetLeftPart(UriPartial.Path);
            output.Attributes.SetAttribute("src", QueryHelpers.AddQueryString(origin, parameters));
            var variants = new List<string>();
            foreach (var candidate in new[] { 480, 800, 1200 }.Where(w => w <= width).Append(width).Distinct())
            {
                parameters["w"] = candidate.ToString();
                variants.Add(QueryHelpers.AddQueryString(origin, parameters) + " " + candidate + "w");
            }
            output.Attributes.SetAttribute("srcset", string.Join(", ", variants));
            SetDefault(output, "sizes", "(max-width: 767px) 100vw, 50vw");
            SetDefault(output, "width", width);
            SetDefault(output, "height", Math.Round(width * 2d / 3d));
        }
        else
        {
            SetDefault(output, "width", 1200);
            SetDefault(output, "height", 800);
        }

        SetDefault(output, "alt", "");
        SetDefault(output, "decoding", "async");
        SetDefault(output, "loading", output.Attributes["fetchpriority"]?.Value?.ToString() == "high" ? "eager" : "lazy");
    }

    private static void SetDefault(TagHelperOutput output, string name, object value)
    {
        if (!output.Attributes.ContainsName(name)) output.Attributes.SetAttribute(name, value);
    }
}
