using Microsoft.EntityFrameworkCore.Migrations;

namespace Orient.Base.Net.Core.Api.Migrations
{
    public partial class Beverage_v15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "FeeShip",
                table: "Product",
                newName: "Image");

            migrationBuilder.AddColumn<string>(
                name: "FeeShip",
                table: "Shop",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeeShip",
                table: "Shop");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Product",
                newName: "FeeShip");

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Product",
                nullable: true);
        }
    }
}
