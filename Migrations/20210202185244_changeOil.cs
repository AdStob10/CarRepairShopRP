using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRepairShopRP.Migrations
{
    public partial class changeOil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ChangeOil",
                table: "Repair",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangeOil",
                table: "Repair");
        }
    }
}
