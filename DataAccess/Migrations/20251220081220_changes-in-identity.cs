using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changesinidentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "ForumThreads",
                newName: "TitleFa");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ForumCategories",
                newName: "NameFa");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "BlogPosts",
                newName: "TitleFa");

            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "BlogPosts",
                newName: "TitleEn");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "BlogPosts",
                newName: "SummaryFa");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "BlogCategories",
                newName: "NameFa");

            migrationBuilder.AddColumn<string>(
                name: "TitleEn",
                table: "ForumThreads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                table: "ForumCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContentEn",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContentFa",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SummaryEn",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                table: "BlogCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TitleEn",
                table: "ForumThreads");

            migrationBuilder.DropColumn(
                name: "NameEn",
                table: "ForumCategories");

            migrationBuilder.DropColumn(
                name: "ContentEn",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "ContentFa",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "SummaryEn",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "NameEn",
                table: "BlogCategories");

            migrationBuilder.RenameColumn(
                name: "TitleFa",
                table: "ForumThreads",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "NameFa",
                table: "ForumCategories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TitleFa",
                table: "BlogPosts",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "TitleEn",
                table: "BlogPosts",
                newName: "Summary");

            migrationBuilder.RenameColumn(
                name: "SummaryFa",
                table: "BlogPosts",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "NameFa",
                table: "BlogCategories",
                newName: "Name");
        }
    }
}
