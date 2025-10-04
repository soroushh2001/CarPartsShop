using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarPartsShop.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCarBrandStruc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_CarBrands_CarBrandId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CarBrandId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CarBrandId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "ProductCarBrand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarBrandId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCarBrand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCarBrand_CarBrands_CarBrandId",
                        column: x => x.CarBrandId,
                        principalTable: "CarBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCarBrand_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCarBrand_CarBrandId",
                table: "ProductCarBrand",
                column: "CarBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCarBrand_ProductId",
                table: "ProductCarBrand",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCarBrand");

            migrationBuilder.AddColumn<int>(
                name: "CarBrandId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CarBrandId",
                table: "Products",
                column: "CarBrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CarBrands_CarBrandId",
                table: "Products",
                column: "CarBrandId",
                principalTable: "CarBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
