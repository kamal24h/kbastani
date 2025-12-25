using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addaboutresume : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tags",
                newName: "NameFa");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Skills",
                newName: "NameFa");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Projects",
                newName: "TitleFa");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Projects",
                newName: "TitleEn");

            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionFa",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GithubUrl",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectUrl",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailPath",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "AppUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.CreateTable(
                name: "Abouts",
                columns: table => new
                {
                    AboutId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AboutGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleFa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BioFa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BioEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationFa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkedinUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GithubUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abouts", x => x.AboutId);
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    EducationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EducationGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DegreeFa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DegreeEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniversityFa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniversityEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionFa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.EducationId);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    ExperienceId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExperienceGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobTitleFa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobTitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyFa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionFa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.ExperienceId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abouts");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropColumn(
                name: "NameEn",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "NameEn",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DescriptionFa",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "GithubUrl",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectUrl",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ThumbnailPath",
                table: "BlogPosts");

            migrationBuilder.RenameColumn(
                name: "NameFa",
                table: "Tags",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "NameFa",
                table: "Skills",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TitleFa",
                table: "Projects",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "TitleEn",
                table: "Projects",
                newName: "Description");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "AppUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);
        }
    }
}
