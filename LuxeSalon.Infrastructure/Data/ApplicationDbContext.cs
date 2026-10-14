using LuxeSalon.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuxeSalon.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Service> Services { get; set; } = null!;
    public DbSet<Appointment> Appointments { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<BlogPost> BlogPosts { get; set; } = null!;
    public DbSet<SiteImage> SiteImages { get; set; } = null!;
    public DbSet<TeamMember> TeamMembers { get; set; } = null!;
    public DbSet<PriceItem> PriceItems { get; set; } = null!;
    public DbSet<SiteContent> SiteContents { get; set; } = null!;
    public DbSet<SiteSetting> SiteSettings { get; set; } = null!;
    public DbSet<Testimonial> Testimonials { get; set; } = null!;
    public DbSet<GalleryItem> GalleryItems { get; set; } = null!;
    public DbSet<MarqueeItem> MarqueeItems { get; set; } = null!;
    public DbSet<StatItem> StatItems { get; set; } = null!;
    public DbSet<AwardItem> AwardItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Service>()
            .Property(s => s.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<PriceItem>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<BlogPost>()
            .HasIndex(b => b.Slug)
            .IsUnique();

        modelBuilder.Entity<BlogPost>()
            .HasIndex(b => b.SlugEn);

        modelBuilder.Entity<Service>()
            .HasIndex(s => s.Slug);

        modelBuilder.Entity<Service>()
            .HasIndex(s => s.SlugEn);

        modelBuilder.Entity<SiteContent>()
            .HasIndex(c => new { c.Key, c.Culture })
            .IsUnique();
    }
}
