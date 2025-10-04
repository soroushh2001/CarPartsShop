using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarPartsShop.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixCarBrand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_CarBrand_CarBrandId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarBrand",
                table: "CarBrand");

            migrationBuilder.RenameTable(
                name: "CarBrand",
                newName: "CarBrands");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarBrands",
                table: "CarBrands",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CarBrands_CarBrandId",
                table: "Products",
                column: "CarBrandId",
                principalTable: "CarBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_CarBrands_CarBrandId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarBrands",
                table: "CarBrands");

            migrationBuilder.RenameTable(
                name: "CarBrands",
                newName: "CarBrand");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarBrand",
                table: "CarBrand",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CarBrand_CarBrandId",
                table: "Products",
                column: "CarBrandId",
                principalTable: "CarBrand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
