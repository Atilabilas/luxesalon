using LuxeSalon.Web.TagHelpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.FileProviders;
using Xunit;

namespace LuxeSalon.Tests;

public class ImageOptimizationTests
{
    [Fact]
    public void EncodedImageUrl_KeepsRealQueryParameters()
    {
        var attributes = new TagHelperAttributeList
        {
            { "src", new HtmlString("https://images.unsplash.com/photo-custom?q=80&amp;w=800&amp;auto=format&amp;fit=crop") }
        };
        var context = new TagHelperContext(attributes, new Dictionary<object, object>(), "test");
        var output = new TagHelperOutput("img", attributes, (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

        new OptimizedImageTagHelper(new ImageAssets(new TestEnvironment())).Process(context, output);

        var url = output.Attributes["src"].Value.ToString()!;
        Assert.DoesNotContain("amp;", url);
        Assert.Contains("w=800", url);
        Assert.Contains("fm=webp", url);
        Assert.DoesNotContain("auto=", url);
    }

    private sealed class TestEnvironment : IWebHostEnvironment
    {
        public string ApplicationName { get; set; } = "Test";
        public IFileProvider WebRootFileProvider { get; set; } = new NullFileProvider();
        public string WebRootPath { get; set; } = "missing-test-webroot";
        public string EnvironmentName { get; set; } = "Test";
        public string ContentRootPath { get; set; } = ".";
        public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
    }
}
