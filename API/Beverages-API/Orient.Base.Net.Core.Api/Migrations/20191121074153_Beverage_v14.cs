using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orient.Base.Net.Core.Api.Migrations
{
    public partial class Beverage_v14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                table: "Product");

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<Guid>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: true),
                    RecordActive = table.Column<bool>(nullable: false),
                    RecordDeleted = table.Column<bool>(nullable: false),
                    RecordOrder = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Url = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Image_ProductId",
                table: "Image",
                column: "ProductId");
        }
    }
}
