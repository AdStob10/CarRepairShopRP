using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRepairShopRP.Migrations
{
    public partial class extendVisit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Accepted",
                table: "Visit",
                newName: "AcceptedMechanic");

            migrationBuilder.AddColumn<bool>(
                name: "AcceptedClient",
                table: "Visit",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VisitMechanicId",
                table: "Visit",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visit_VisitMechanicId",
                table: "Visit",
                column: "VisitMechanicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visit_AspNetUsers_VisitMechanicId",
                table: "Visit",
                column: "VisitMechanicId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visit_AspNetUsers_VisitMechanicId",
                table: "Visit");

            migrationBuilder.DropIndex(
                name: "IX_Visit_VisitMechanicId",
                table: "Visit");

            migrationBuilder.DropColumn(
                name: "AcceptedClient",
                table: "Visit");

            migrationBuilder.DropColumn(
                name: "VisitMechanicId",
                table: "Visit");

            migrationBuilder.RenameColumn(
                name: "AcceptedMechanic",
                table: "Visit",
                newName: "Accepted");
        }
    }
}
