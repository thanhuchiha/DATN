using Microsoft.EntityFrameworkCore.Migrations;

namespace Orient.Base.Net.Core.Api.Migrations
{
    public partial class Beverage_v16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInCategory",
                table: "ProductInCategory");

            migrationBuilder.DropIndex(
                name: "IX_ProductInCategory_ProductId",
                table: "ProductInCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInCategory",
                table: "ProductInCategory",
                columns: new[] { "ProductId", "CategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductInCategory_CategoryId",
                table: "ProductInCategory",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInCategory",
                table: "ProductInCategory");

            migrationBuilder.DropIndex(
                name: "IX_ProductInCategory_CategoryId",
                table: "ProductInCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInCategory",
                table: "ProductInCategory",
                columns: new[] { "CategoryId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductInCategory_ProductId",
                table: "ProductInCategory",
                column: "ProductId");
        }
    }
}
