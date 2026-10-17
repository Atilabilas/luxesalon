using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Web.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using System.Globalization;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

// Opt-in for isolated previews where the Windows profile key store is unavailable.
if (builder.Environment.IsDevelopment() && builder.Configuration.GetValue<bool>("Preview:EphemeralKeys"))
    builder.Services.AddDataProtection().UseEphemeralDataProtectionProvider();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Login";
        options.LogoutPath = "/Admin/Logout";
        options.AccessDeniedPath = "/Admin/Login";
        options.ExpireTimeSpan = TimeSpan.FromHours(24);
        options.SlidingExpiration = true;
        options.Cookie.Name = "LuxeSalon.Admin";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    });

builder.Services.AddAuthorization();

builder.Services.AddRazorPages(options =>
    {
        options.Conventions.AuthorizeFolder("/Admin");
        options.Conventions.AllowAnonymousToPage("/Admin/Login");
        options.Conventions.AllowAnonymousToPage("/Admin/Logout");

        // Canonical TR + EN routes (single @page per file; extras via conventions)
        options.Conventions.AddPageRoute("/Index", "en");
        options.Conventions.AddPageRoute("/Hakkimizda", "en/about");
        options.Conventions.AddPageRoute("/Hizmetler", "en/services");
        options.Conventions.AddPageRoute("/Hizmetler/Details", "en/services/{slug}");
        options.Conventions.AddPageRoute("/Blog/Index", "en/blog");
        options.Conventions.AddPageRoute("/Blog/Details", "en/blog/{slug}");
        options.Conventions.AddPageRoute("/Randevu", "en/appointment");
        options.Conventions.AddPageRoute("/Privacy", "en/privacy");
        options.Conventions.AddPageRoute("/IzmirIlceleri", "en/izmir-districts");
        options.Conventions.AddPageRoute("/Shop/Index", "en/shop");
    })
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

var supportedCultures = new[]
{
    new CultureInfo("tr"),
    new CultureInfo("en")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("tr");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new CustomRequestCultureProvider(context =>
        {
            var path = context.Request.Path.Value ?? "/";
            var culture = path.Equals("/en", StringComparison.OrdinalIgnoreCase) ||
                          path.StartsWith("/en/", StringComparison.OrdinalIgnoreCase)
                ? "en"
                : "tr";
            return Task.FromResult<ProviderCultureResult?>(new ProviderCultureResult(culture, culture));
        }),
        new CookieRequestCultureProvider(),
        new QueryStringRequestCultureProvider()
    };
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=App_Data/LuxeSalon.db";
if (connectionString.Contains("App_Data/"))
{
    var parts = connectionString.Split("App_Data/");
    if (parts.Length > 1)
    {
        var dbName = parts[1];
        var dbPath = Path.Combine(builder.Environment.ContentRootPath, "App_Data", dbName);
        Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);
        connectionString = $"Data Source={dbPath}";
    }
}
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddMemoryCache();
builder.Services.AddSingleton<LuxeSalon.Web.TagHelpers.ImageAssets>();
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.MimeTypes = Microsoft.AspNetCore.ResponseCompression.ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/xml", "application/ld+json" });
});
builder.Services.AddScoped<LuxeSalon.Web.Services.SiteSettingsService>();
builder.Services.AddScoped<LuxeSalon.Web.Services.ContentService>();

var app = builder.Build();

app.UseForwardedHeaders();
app.UseResponseCompression();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/sayfa-bulunamadi");
app.UseRequestLocalization();
app.UseMiddleware<CanonicalRedirectMiddleware>();
app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

var staticContentTypes = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
staticContentTypes.Mappings[".webmanifest"] = "application/manifest+json";

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = staticContentTypes,
    OnPrepareResponse = ctx =>
    {
        var path = ctx.File.Name;
        if (path.EndsWith(".css", StringComparison.OrdinalIgnoreCase) ||
            path.EndsWith(".js", StringComparison.OrdinalIgnoreCase) ||
            path.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
            path.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
            path.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
            path.EndsWith(".webp", StringComparison.OrdinalIgnoreCase) ||
            path.EndsWith(".svg", StringComparison.OrdinalIgnoreCase) ||
            path.EndsWith(".ico", StringComparison.OrdinalIgnoreCase) ||
            path.EndsWith(".woff2", StringComparison.OrdinalIgnoreCase) ||
            path.EndsWith(".woff", StringComparison.OrdinalIgnoreCase))
        {
            ctx.Context.Response.Headers.CacheControl = "public,max-age=31536000,immutable";
        }
    }
});

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
    // Seed only a new database. CMS edits and publication dates must survive restarts.
    if (!context.SiteSettings.Any())
        DataSeeder.Seed(context);
    BlogEditorialContent.Apply(context);
}

app.Run();

public partial class Program { }
