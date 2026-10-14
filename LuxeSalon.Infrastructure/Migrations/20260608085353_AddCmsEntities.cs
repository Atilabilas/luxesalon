using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuxeSalon.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCmsEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "SiteContents",
                type: "TEXT",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AwardItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Icon = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Subtitle = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Year = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    DisplayOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwardItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GalleryItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Section = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    OverlayText = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    LinkUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    DisplayOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarqueeItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    DisplayOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarqueeItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Label = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Section = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DisplayOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Testimonials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Quote = table.Column<string>(type: "TEXT", nullable: false),
                    AuthorName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    AuthorTitle = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    DisplayOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testimonials", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AwardItems");

            migrationBuilder.DropTable(
                name: "GalleryItems");

            migrationBuilder.DropTable(
                name: "MarqueeItems");

            migrationBuilder.DropTable(
                name: "StatItems");

            migrationBuilder.DropTable(
                name: "Testimonials");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "SiteContents");
        }
    }
}
