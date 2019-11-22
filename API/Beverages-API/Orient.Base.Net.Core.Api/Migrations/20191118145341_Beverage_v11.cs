using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orient.Base.Net.Core.Api.Migrations
{
    public partial class Beverage_v11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ShopId",
                table: "User",
                nullable: true,
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ShopId",
                table: "User",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
