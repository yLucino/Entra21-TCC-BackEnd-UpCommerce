using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entra21_TCC_BackEnd_UpCommerce.Migrations
{
    /// <inheritdoc />
    public partial class InitialProjectSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cdks_Cdks_CdkId1",
                table: "Cdks");

            migrationBuilder.DropForeignKey(
                name: "FK_Cdks_Styles_StyleId",
                table: "Cdks");

            migrationBuilder.DropIndex(
                name: "IX_Cdks_StyleId",
                table: "Cdks");

            migrationBuilder.DropColumn(
                name: "StyleId",
                table: "Cdks");

            migrationBuilder.RenameColumn(
                name: "CdkId1",
                table: "Cdks",
                newName: "ParentCdkId");

            migrationBuilder.RenameIndex(
                name: "IX_Cdks_CdkId1",
                table: "Cdks",
                newName: "IX_Cdks_ParentCdkId");

            migrationBuilder.AddColumn<int>(
                name: "CdkId",
                table: "Styles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Styles_CdkId",
                table: "Styles",
                column: "CdkId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cdks_Cdks_ParentCdkId",
                table: "Cdks",
                column: "ParentCdkId",
                principalTable: "Cdks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Styles_Cdks_CdkId",
                table: "Styles",
                column: "CdkId",
                principalTable: "Cdks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cdks_Cdks_ParentCdkId",
                table: "Cdks");

            migrationBuilder.DropForeignKey(
                name: "FK_Styles_Cdks_CdkId",
                table: "Styles");

            migrationBuilder.DropIndex(
                name: "IX_Styles_CdkId",
                table: "Styles");

            migrationBuilder.DropColumn(
                name: "CdkId",
                table: "Styles");

            migrationBuilder.RenameColumn(
                name: "ParentCdkId",
                table: "Cdks",
                newName: "CdkId1");

            migrationBuilder.RenameIndex(
                name: "IX_Cdks_ParentCdkId",
                table: "Cdks",
                newName: "IX_Cdks_CdkId1");

            migrationBuilder.AddColumn<int>(
                name: "StyleId",
                table: "Cdks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cdks_StyleId",
                table: "Cdks",
                column: "StyleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cdks_Cdks_CdkId1",
                table: "Cdks",
                column: "CdkId1",
                principalTable: "Cdks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cdks_Styles_StyleId",
                table: "Cdks",
                column: "StyleId",
                principalTable: "Styles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
