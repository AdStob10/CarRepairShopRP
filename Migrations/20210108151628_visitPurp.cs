using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRepairShopRP.Migrations
{
    public partial class visitPurp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "plannedVisitDate",
                table: "Visit",
                newName: "PlannedVisitDate");

            migrationBuilder.RenameColumn(
                name: "accepted",
                table: "Visit",
                newName: "Accepted");

            migrationBuilder.AddColumn<string>(
                name: "VisitPurpose",
                table: "Visit",
                type: "nvarchar(140)",
                maxLength: 140,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VisitPurpose",
                table: "Visit");

            migrationBuilder.RenameColumn(
                name: "PlannedVisitDate",
                table: "Visit",
                newName: "plannedVisitDate");

            migrationBuilder.RenameColumn(
                name: "Accepted",
                table: "Visit",
                newName: "accepted");
        }
    }
}
