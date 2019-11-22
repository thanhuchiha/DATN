using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orient.Base.Net.Core.Api.Migrations
{
    public partial class Beverage_v13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInShops_Product_ProductId",
                table: "ProductInShops");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInShops_Shop_ShopId",
                table: "ProductInShops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInShops",
                table: "ProductInShops");

            migrationBuilder.RenameTable(
                name: "ProductInShops",
                newName: "ProductInShop");

            migrationBuilder.RenameIndex(
                name: "IX_ProductInShops_ProductId",
                table: "ProductInShop",
                newName: "IX_ProductInShop_ProductId");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "ProductInShop",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ProductInShop",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "ProductInShop",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "ProductInShop",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ProductInShop",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "RecordActive",
                table: "ProductInShop",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RecordDeleted",
                table: "ProductInShop",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RecordOrder",
                table: "ProductInShop",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "ProductInShop",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "ProductInShop",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ProductInShop_Id",
                table: "ProductInShop",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInShop",
                table: "ProductInShop",
                columns: new[] { "ShopId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInShop_Product_ProductId",
                table: "ProductInShop",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInShop_Shop_ShopId",
                table: "ProductInShop",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInShop_Product_ProductId",
                table: "ProductInShop");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInShop_Shop_ShopId",
                table: "ProductInShop");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ProductInShop_Id",
                table: "ProductInShop");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInShop",
                table: "ProductInShop");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProductInShop");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ProductInShop");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "ProductInShop");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "ProductInShop");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductInShop");

            migrationBuilder.DropColumn(
                name: "RecordActive",
                table: "ProductInShop");

            migrationBuilder.DropColumn(
                name: "RecordDeleted",
                table: "ProductInShop");

            migrationBuilder.DropColumn(
                name: "RecordOrder",
                table: "ProductInShop");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ProductInShop");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "ProductInShop");

            migrationBuilder.RenameTable(
                name: "ProductInShop",
                newName: "ProductInShops");

            migrationBuilder.RenameIndex(
                name: "IX_ProductInShop_ProductId",
                table: "ProductInShops",
                newName: "IX_ProductInShops_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInShops",
                table: "ProductInShops",
                columns: new[] { "ShopId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInShops_Product_ProductId",
                table: "ProductInShops",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInShops_Shop_ShopId",
                table: "ProductInShops",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
