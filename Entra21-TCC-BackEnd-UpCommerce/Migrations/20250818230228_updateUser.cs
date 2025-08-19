using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entra21_TCC_BackEnd_UpCommerce.Migrations
{
    /// <inheritdoc />
    public partial class updateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "urlInstagram",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "urlLinkedin",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "urlPhoto",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "urlInstagram",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "urlLinkedin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "urlPhoto",
                table: "Users");
        }
    }
}
