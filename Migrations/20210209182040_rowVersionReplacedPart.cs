using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRepairShopRP.Migrations
{
    public partial class rowVersionReplacedPart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ReplacedPart",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ReplacedPart");
        }
    }
}
