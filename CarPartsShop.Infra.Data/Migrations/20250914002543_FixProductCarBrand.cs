using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarPartsShop.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixProductCarBrand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCarBrand_CarBrands_CarBrandId",
                table: "ProductCarBrand");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCarBrand_Products_ProductId",
                table: "ProductCarBrand");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCarBrand",
                table: "ProductCarBrand");

            migrationBuilder.RenameTable(
                name: "ProductCarBrand",
                newName: "ProductCarBrands");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCarBrand_ProductId",
                table: "ProductCarBrands",
                newName: "IX_ProductCarBrands_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCarBrand_CarBrandId",
                table: "ProductCarBrands",
                newName: "IX_ProductCarBrands_CarBrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCarBrands",
                table: "ProductCarBrands",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCarBrands_CarBrands_CarBrandId",
                table: "ProductCarBrands",
                column: "CarBrandId",
                principalTable: "CarBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCarBrands_Products_ProductId",
                table: "ProductCarBrands",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCarBrands_CarBrands_CarBrandId",
                table: "ProductCarBrands");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCarBrands_Products_ProductId",
                table: "ProductCarBrands");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCarBrands",
                table: "ProductCarBrands");

            migrationBuilder.RenameTable(
                name: "ProductCarBrands",
                newName: "ProductCarBrand");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCarBrands_ProductId",
                table: "ProductCarBrand",
                newName: "IX_ProductCarBrand_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCarBrands_CarBrandId",
                table: "ProductCarBrand",
                newName: "IX_ProductCarBrand_CarBrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCarBrand",
                table: "ProductCarBrand",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCarBrand_CarBrands_CarBrandId",
                table: "ProductCarBrand",
                column: "CarBrandId",
                principalTable: "CarBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCarBrand_Products_ProductId",
                table: "ProductCarBrand",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
