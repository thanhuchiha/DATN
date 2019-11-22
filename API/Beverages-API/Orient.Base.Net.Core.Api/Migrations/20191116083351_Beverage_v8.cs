using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orient.Base.Net.Core.Api.Migrations
{
    public partial class Beverage_v8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInShop");

            migrationBuilder.AddColumn<Guid>(
                name: "ShopId",
                table: "User",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_User_ShopId",
                table: "User",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Shop_ShopId",
                table: "User",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Shop_ShopId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ShopId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "User");

            migrationBuilder.CreateTable(
                name: "UserInShop",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    ShopId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInShop", x => new { x.UserId, x.ShopId });
                    table.ForeignKey(
                        name: "FK_UserInShop_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInShop_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserInShop_ShopId",
                table: "UserInShop",
                column: "ShopId");
        }
    }
}
