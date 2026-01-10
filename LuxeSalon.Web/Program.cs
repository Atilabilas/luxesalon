using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add Cookie Authentication
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

// Add services to the container.
builder.Services.AddRazorPages(options =>
    {
        // Admin klasörü altındaki tüm sayfalar için yetkilendirme gerekli
        options.Conventions.AuthorizeFolder("/Admin");
        // Login ve Logout sayfaları herkese açık
        options.Conventions.AllowAnonymousToPage("/Admin/Login");
        options.Conventions.AllowAnonymousToPage("/Admin/Logout");
    })
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

// Add Localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Configure supported cultures
var supportedCultures = new[]
{
    new CultureInfo("tr"),
    new CultureInfo("en"),
    new CultureInfo("de"),
    new CultureInfo("fr"),
    new CultureInfo("ru"),
    new CultureInfo("ar")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("tr");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new QueryStringRequestCultureProvider(),
        new CookieRequestCultureProvider(),
        new AcceptLanguageHeaderRequestCultureProvider()
    };
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=App_Data/LuxeSalon.db";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString)
           .ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning)));

builder.Services.AddMemoryCache();
builder.Services.AddScoped<LuxeSalon.Web.Services.SiteSettingsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Use Request Localization
app.UseRequestLocalization();

app.UseRouting();

// Authentication ve Authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.MapRazorPages();

// Seed data if database is empty
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Ensure database is created and migrations are applied
    context.Database.Migrate();

    if (!context.BlogPosts.Any())
    {
        context.BlogPosts.Add(new BlogPost { 
            Title = "Mimari Saç Tasarımında Yeni Dönem", 
            Summary = "Celill Kuaför olarak 2025 editoryal çekimlerinde neden minimalist ve yapılandırılmış kesimlerin öne çıktığını inceliyoruz.",
            Content = "<p>Güzellik anlayışımızı sadelik üzerine kuruyoruz...</p>",
            Author = "Celil Yılmaz",
            HeaderImageUrl = "/images/portfolio-1.png",
            PublishedAt = DateTime.UtcNow.AddDays(-2)
        });
        context.BlogPosts.Add(new BlogPost { 
            Title = "Işığın Saçtaki Dansı: Artisan Renklendirme", 
            Summary = "Yeni geliştirdiğimiz renklendirme tekniği ile en karanlık ortamlarda bile ışığı yakalayan saçlar.",
            Content = "<p>Sadece bir renk değil, bir ışık oyunu sunuyoruz...</p>",
            Author = "Celil Yılmaz",
            HeaderImageUrl = "/images/hero-1.png",
            PublishedAt = DateTime.UtcNow.AddDays(-10)
        });
        context.SaveChanges();
    }

    if (!context.Products.Any())
    {
        context.Products.Add(new Product { Name = "Sculpting Clay No. 04", Price = 850, Category = "Styling", Description = "Mimari yapı için mat bitiş. Profesyonel kalite.", ImageUrl = "https://images.unsplash.com/photo-1594465919760-441fe5908ab0?q=80&w=400&auto=format&fit=crop" });
        context.Products.Add(new Product { Name = "Silk Infusion Serum", Price = 1200, Category = "Hair Care", Description = "Moleküler onarım ve parlaklık serumu.", ImageUrl = "https://images.unsplash.com/photo-1608248597279-f99d160bfcbc?q=80&w=400&auto=format&fit=crop" });
        context.Products.Add(new Product { Name = "Editorial Volume Mist", Price = 950, Category = "Styling", Description = "Yüksek moda hareketliliği için hafif hacim spreyi.", ImageUrl = "https://images.unsplash.com/photo-1571575173749-b39bc51341d1?q=80&w=400&auto=format&fit=crop" });
        context.Products.Add(new Product { Name = "Keratin Repair Mask", Price = 1450, Category = "Hair Care", Description = "Yoğun keratin bakım maskesi. Hasarlı saçlar için.", ImageUrl = "https://images.unsplash.com/photo-1526947425960-945c6e72858f?q=80&w=400&auto=format&fit=crop" });
        context.Products.Add(new Product { Name = "Color Protect Şampuan", Price = 680, Category = "Hair Care", Description = "Boyalı saçlar için renk koruyucu şampuan.", ImageUrl = "https://images.unsplash.com/photo-1585751119414-ef2636f8aede?q=80&w=400&auto=format&fit=crop" });
        context.Products.Add(new Product { Name = "Argan Oil Elixir", Price = 890, Category = "Hair Care", Description = "Saf Argan yağı ile besleyici serum.", ImageUrl = "https://images.unsplash.com/photo-1556228720-195a672e8a03?q=80&w=400&auto=format&fit=crop" });
        context.SaveChanges();
    }

    if (!context.Services.Any())
    {
        // Kesim Kategorisi
        context.Services.Add(new Service { Name = "Signature Cut", Description = "Kemik yapısına uygun mimari kesim.", Price = 1200, Category = "Kesim", DurationMinutes = 60, ImageUrl = "https://images.unsplash.com/photo-1560066984-138dadb4c035?q=80&w=800&auto=format&fit=crop" });
        context.Services.Add(new Service { Name = "Style & Blow Dry", Description = "Profesyonel fön ve şekillendirme.", Price = 600, Category = "Kesim", DurationMinutes = 45, ImageUrl = "https://images.unsplash.com/photo-1522337360788-8b13dee7a37e?q=80&w=800&auto=format&fit=crop" });
        context.Services.Add(new Service { Name = "Pony & Trim", Description = "Kahkül kesimi ve uç alma.", Price = 400, Category = "Kesim", DurationMinutes = 30, ImageUrl = "https://images.unsplash.com/photo-1595959183082-7b570b7e1dfa?q=80&w=800&auto=format&fit=crop" });
        
        // Renk Kategorisi
        context.Services.Add(new Service { Name = "Artisan Balayage", Description = "El boyaması tekniği ile doğal geçişler.", Price = 3500, Category = "Renk", DurationMinutes = 180, ImageUrl = "https://images.unsplash.com/photo-1522337360788-8b13dee7a37e?q=80&w=800&auto=format&fit=crop" });
        context.Services.Add(new Service { Name = "Global Color", Description = "Tam saç boyama işlemi.", Price = 2000, Category = "Renk", DurationMinutes = 120, ImageUrl = "https://images.unsplash.com/photo-1562322140-8baeececf3df?q=80&w=800&auto=format&fit=crop" });
        context.Services.Add(new Service { Name = "Highlights", Description = "İnce röfle ve highlight.", Price = 2500, Category = "Renk", DurationMinutes = 150, ImageUrl = "https://images.unsplash.com/photo-1580618672591-eb180b1a973f?q=80&w=800&auto=format&fit=crop" });
        
        // Bakım Kategorisi
        context.Services.Add(new Service { Name = "Keratin Treatment", Description = "Brezilya keratin bakımı.", Price = 4500, Category = "Bakım", DurationMinutes = 180, ImageUrl = "https://images.unsplash.com/photo-1522337660859-02fbefca4702?q=80&w=800&auto=format&fit=crop" });
        context.Services.Add(new Service { Name = "Deep Moisture Ritual", Description = "Yoğun nem bakımı.", Price = 800, Category = "Bakım", DurationMinutes = 45, ImageUrl = "https://images.unsplash.com/photo-1559599101-f09722fb4948?q=80&w=800&auto=format&fit=crop" });
        context.Services.Add(new Service { Name = "Scalp Detox", Description = "Saç derisi detoksu.", Price = 650, Category = "Bakım", DurationMinutes = 40, ImageUrl = "https://images.unsplash.com/photo-1487412720507-e7ab37603c6f?q=80&w=800&auto=format&fit=crop" });
        context.SaveChanges();
    }

    // Seed Team Members
    if (!context.TeamMembers.Any())
    {
        context.TeamMembers.Add(new TeamMember { 
            Name = "Celil Yılmaz", 
            Role = "Kurucu & Baş Stilist", 
            Bio = "15 yıllık deneyim. Paris ve Milano'da eğitim.", 
            ImageUrl = "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?q=80&w=600&auto=format&fit=crop",
            DisplayOrder = 1,
            IsActive = true
        });
        context.TeamMembers.Add(new TeamMember { 
            Name = "Ayşe Kaya", 
            Role = "Renk Uzmanı", 
            Bio = "Balayage ve ombre tekniklerinde uzman.", 
            ImageUrl = "https://images.unsplash.com/photo-1580618672591-eb180b1a973f?q=80&w=600&auto=format&fit=crop",
            DisplayOrder = 2,
            IsActive = true
        });
        context.TeamMembers.Add(new TeamMember { 
            Name = "Mehmet Demir", 
            Role = "Kesim Uzmanı", 
            Bio = "Modern ve klasik kesim teknikleri.", 
            ImageUrl = "https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?q=80&w=600&auto=format&fit=crop",
            DisplayOrder = 3,
            IsActive = true
        });
        context.TeamMembers.Add(new TeamMember { 
            Name = "Zeynep Arslan", 
            Role = "Makyaj Artisti", 
            Bio = "Gelin ve özel gün makyajı uzmanı.", 
            ImageUrl = "https://images.unsplash.com/photo-1494790108377-be9c29b29330?q=80&w=600&auto=format&fit=crop",
            DisplayOrder = 4,
            IsActive = true
        });
        context.SaveChanges();
    }

    // Seed Site Content
    if (!context.SiteContents.Any())
    {
        var contents = new List<SiteContent>
        {
            // Loading Screen
            new() { Key = "loading_text", Section = "Loading", Value = "celil kuaför", ContentType = "text", DisplayOrder = 1 },
            
            // Hero Section
            new() { Key = "hero_subtitle", Section = "Hero", Value = "PREMIUM SAÇ TASARIM STÜDYOSU", ContentType = "text", DisplayOrder = 1 },
            new() { Key = "hero_title", Section = "Hero", Value = "Celil Kuaför", ContentType = "text", DisplayOrder = 2 },
            new() { Key = "hero_cta_appointment", Section = "Hero", Value = "RANDEVU AL", ContentType = "text", DisplayOrder = 3 },
            new() { Key = "hero_cta_services", Section = "Hero", Value = "HİZMETLERİMİZ", ContentType = "text", DisplayOrder = 4 },
            new() { Key = "hero_scroll", Section = "Hero", Value = "KEŞFET", ContentType = "text", DisplayOrder = 5 },
            
            // About Section
            new() { Key = "about_subtitle", Section = "About", Value = "HAKKIMIZDA", ContentType = "text", DisplayOrder = 1 },
            new() { Key = "about_title", Section = "About", Value = "Saç Tasarımında Sanat", ContentType = "text", DisplayOrder = 2 },
            new() { Key = "about_description1", Section = "About", Value = "Celil Kuaför olarak, her müşterimizi benzersiz bir sanat eseri olarak görüyoruz. 2010 yılından bu yana, İstanbul'un kalbinde modern teknikler ve zamansız estetiği bir araya getirerek hizmet veriyoruz.", ContentType = "text", DisplayOrder = 3 },
            new() { Key = "about_description2", Section = "About", Value = "Ekibimiz, uluslararası deneyime sahip stilistlerden oluşmaktadır. Her kesim, her renklendirme sizin kişiliğinizi yansıtan bir imza taşır.", ContentType = "text", DisplayOrder = 4 },
            new() { Key = "about_cta", Section = "About", Value = "DAHA FAZLA", ContentType = "text", DisplayOrder = 5 },
            new() { Key = "about_experience_number", Section = "About", Value = "15+", ContentType = "text", DisplayOrder = 6 },
            new() { Key = "about_experience_text", Section = "About", Value = "YILLIK DENEYİM", ContentType = "text", DisplayOrder = 7 },
            
            // Services Section
            new() { Key = "services_subtitle", Section = "Services", Value = "HİZMETLERİMİZ", ContentType = "text", DisplayOrder = 1 },
            new() { Key = "services_title", Section = "Services", Value = "Ne Sunuyoruz", ContentType = "text", DisplayOrder = 2 },
            new() { Key = "service1_title", Section = "Services", Value = "Saç Kesimi", ContentType = "text", DisplayOrder = 3 },
            new() { Key = "service1_desc", Section = "Services", Value = "Yüz yapınıza özel kesim", ContentType = "text", DisplayOrder = 4 },
            new() { Key = "service2_title", Section = "Services", Value = "Renklendirme", ContentType = "text", DisplayOrder = 5 },
            new() { Key = "service2_desc", Section = "Services", Value = "Balayage, ombre, özel renkler", ContentType = "text", DisplayOrder = 6 },
            new() { Key = "service3_title", Section = "Services", Value = "Saç Bakımı", ContentType = "text", DisplayOrder = 7 },
            new() { Key = "service3_desc", Section = "Services", Value = "Keratin, botox, onarım", ContentType = "text", DisplayOrder = 8 },
            new() { Key = "service4_title", Section = "Services", Value = "Makyaj", ContentType = "text", DisplayOrder = 9 },
            new() { Key = "service4_desc", Section = "Services", Value = "Gelin ve özel gün", ContentType = "text", DisplayOrder = 10 },
            
            // Team Section
            new() { Key = "team_subtitle", Section = "Team", Value = "EKİBİMİZ", ContentType = "text", DisplayOrder = 1 },
            new() { Key = "team_title", Section = "Team", Value = "Uzman Stilistler", ContentType = "text", DisplayOrder = 2 },
            
            // Testimonials Section
            new() { Key = "testimonials_subtitle", Section = "Testimonials", Value = "MÜŞTERİ YORUMLARI", ContentType = "text", DisplayOrder = 1 },
            new() { Key = "testimonial_quote", Section = "Testimonials", Value = "Celil Kuaför'de her ziyaretim bir deneyime dönüşüyor. Sadece saçım değil, kendime olan güvenim de yenileniyor. Ekip gerçekten alanında uzman.", ContentType = "text", DisplayOrder = 2 },
            new() { Key = "testimonial_name", Section = "Testimonials", Value = "Elif Yıldırım", ContentType = "text", DisplayOrder = 3 },
            new() { Key = "testimonial_title", Section = "Testimonials", Value = "Düzenli Müşteri", ContentType = "text", DisplayOrder = 4 },
            
            // Contact Section
            new() { Key = "contact_subtitle", Section = "Contact", Value = "İLETİŞİM", ContentType = "text", DisplayOrder = 1 },
            new() { Key = "contact_title", Section = "Contact", Value = "Bize Ulaşın", ContentType = "text", DisplayOrder = 2 },
            new() { Key = "contact_address_label", Section = "Contact", Value = "Adres", ContentType = "text", DisplayOrder = 3 },
            new() { Key = "contact_address", Section = "Contact", Value = "Alsancak Mah. Kıbrıs Şehitleri Cad. No: 45, Konak, İzmir", ContentType = "text", DisplayOrder = 4 },
            new() { Key = "contact_hours_label", Section = "Contact", Value = "Çalışma Saatleri", ContentType = "text", DisplayOrder = 5 },
            new() { Key = "contact_hours", Section = "Contact", Value = "Pazartesi - Cumartesi: 09:00 - 20:00", ContentType = "text", DisplayOrder = 6 },
            new() { Key = "contact_phone", Section = "Contact", Value = "+90 232 XXX XX XX", ContentType = "text", DisplayOrder = 7 },
            new() { Key = "contact_email", Section = "Contact", Value = "info@celilkuafor.com", ContentType = "text", DisplayOrder = 8 },
            new() { Key = "contact_button", Section = "Contact", Value = "GÖNDER", ContentType = "text", DisplayOrder = 9 },
            
            // CTA Section
            new() { Key = "cta_subtitle", Section = "CTA", Value = "Randevu", ContentType = "text", DisplayOrder = 1 },
            new() { Key = "cta_title", Section = "CTA", Value = "Değişime Hazır Mısın?", ContentType = "text", DisplayOrder = 2 },
            new() { Key = "cta_description", Section = "CTA", Value = "Stilistlerimizle tanışın ve size özel bir deneyim için randevunuzu oluşturun.", ContentType = "text", DisplayOrder = 3 },
        };
        context.SiteContents.AddRange(contents);
        context.SaveChanges();
        context.SaveChanges();
    }

    // Seed SiteSettings
    if (!context.SiteSettings.Any())
    {
        var settings = new List<SiteSetting>
        {
            new() { Key = "Site_Name", Value = "LuxeSalon", Description = "Site Name", Group = "General" },
            new() { Key = "Site_Title", Value = "Premium Hair Salon", Description = "Browser Tab Title Suffix", Group = "General" },
            new() { Key = "Contact_Phone", Value = "+90 532 123 45 67", Description = "Contact Phone Number", Group = "Contact" },
            new() { Key = "Contact_Email", Value = "info@luxesalon.com", Description = "Contact Email Address", Group = "Contact" },
            new() { Key = "Contact_Address", Value = "Alsancak Mah. Kıbrıs Şehitleri Cad. No: 45", Description = "Street Address", Group = "Contact" },
            new() { Key = "Contact_City", Value = "Konak, İzmir", Description = "City/District", Group = "Contact" },
            new() { Key = "Social_Instagram", Value = "#", Description = "Instagram URL", Group = "Social" },
            new() { Key = "Social_Facebook", Value = "#", Description = "Facebook URL", Group = "Social" },
            new() { Key = "Social_Pinterest", Value = "#", Description = "Pinterest URL", Group = "Social" },
            new() { Key = "Whatsapp_Number", Value = "905321234567", Description = "Whatsapp Number (no +)", Group = "Contact" }
        };
        context.SiteSettings.AddRange(settings);
        context.SaveChanges();
    }
}

app.Run();
