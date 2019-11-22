using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orient.Base.Net.Core.Api.Migrations
{
    public partial class Beverage_v12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductInShops",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(nullable: false),
                    ShopId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInShops", x => new { x.ShopId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductInShops_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductInShops_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductInShops_ProductId",
                table: "ProductInShops",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductInShops");
        }
    }
}
