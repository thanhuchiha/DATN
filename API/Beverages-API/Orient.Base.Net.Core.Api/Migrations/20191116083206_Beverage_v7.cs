using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orient.Base.Net.Core.Api.Migrations
{
    public partial class Beverage_v7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shop",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RecordOrder = table.Column<int>(nullable: false),
                    RecordDeleted = table.Column<bool>(nullable: false),
                    RecordActive = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    DeletedBy = table.Column<Guid>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 512, nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    AvatarUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shop", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInShop");

            migrationBuilder.DropTable(
                name: "Shop");
        }
    }
}
