using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarPartsShop.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddParentIdToCarBrands : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarBrandId",
                table: "CarBrands",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "CarBrands",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarBrands_CarBrandId",
                table: "CarBrands",
                column: "CarBrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarBrands_CarBrands_CarBrandId",
                table: "CarBrands",
                column: "CarBrandId",
                principalTable: "CarBrands",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarBrands_CarBrands_CarBrandId",
                table: "CarBrands");

            migrationBuilder.DropIndex(
                name: "IX_CarBrands_CarBrandId",
                table: "CarBrands");

            migrationBuilder.DropColumn(
                name: "CarBrandId",
                table: "CarBrands");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "CarBrands");
        }
    }
}
