using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuxeSalon.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeoSlugsAndCulture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Culture",
                table: "SiteContents",
                type: "TEXT",
                maxLength: 10,
                nullable: false,
                defaultValue: "tr");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                table: "Services",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLandingPage",
                table: "Services",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Services",
                type: "INTEGER",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "LongDescription",
                table: "Services",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LongDescriptionEn",
                table: "Services",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaDescription",
                table: "Services",
                type: "TEXT",
                maxLength: 320,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaDescriptionEn",
                table: "Services",
                type: "TEXT",
                maxLength: 320,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                table: "Services",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTitle",
                table: "Services",
                type: "TEXT",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTitleEn",
                table: "Services",
                type: "TEXT",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Services",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SlugEn",
                table: "Services",
                type: "TEXT",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Services",
                type: "TEXT",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Services",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ContentEn",
                table: "BlogPosts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "BlogPosts",
                type: "TEXT",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SlugEn",
                table: "BlogPosts",
                type: "TEXT",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SummaryEn",
                table: "BlogPosts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "BlogPosts",
                type: "TEXT",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleEn",
                table: "BlogPosts",
                type: "TEXT",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "BlogPosts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.Sql("UPDATE BlogPosts SET Slug = 'yazi-' || Id WHERE Slug IS NULL OR Slug = ''");
            migrationBuilder.Sql("UPDATE Services SET Slug = 'hizmet-' || Id WHERE Slug IS NULL OR Slug = ''");
            migrationBuilder.Sql("UPDATE Services SET IsPublished = 1");
            migrationBuilder.Sql("UPDATE SiteContents SET Culture = 'tr' WHERE Culture IS NULL OR Culture = ''");

            migrationBuilder.CreateIndex(
                name: "IX_SiteContents_Key_Culture",
                table: "SiteContents",
                columns: new[] { "Key", "Culture" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_Slug",
                table: "Services",
                column: "Slug");

            migrationBuilder.CreateIndex(
                name: "IX_Services_SlugEn",
                table: "Services",
                column: "SlugEn");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_Slug",
                table: "BlogPosts",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_SlugEn",
                table: "BlogPosts",
                column: "SlugEn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SiteContents_Key_Culture",
                table: "SiteContents");

            migrationBuilder.DropIndex(
                name: "IX_Services_Slug",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_SlugEn",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_Slug",
                table: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_SlugEn",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "Culture",
                table: "SiteContents");

            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "IsLandingPage",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "LongDescription",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "LongDescriptionEn",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "MetaDescription",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "MetaDescriptionEn",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "NameEn",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "SeoTitle",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "SeoTitleEn",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "SlugEn",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ContentEn",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "SlugEn",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "SummaryEn",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "TitleEn",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BlogPosts");
        }
    }
}
