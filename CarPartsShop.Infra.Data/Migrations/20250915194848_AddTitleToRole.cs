using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarPartsShop.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTitleToRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "AspNetRoles");
        }
    }
}
