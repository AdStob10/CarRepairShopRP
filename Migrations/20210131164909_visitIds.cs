using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRepairShopRP.Migrations
{
    public partial class visitIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visit_AspNetUsers_VisitClientId",
                table: "Visit");

            migrationBuilder.DropForeignKey(
                name: "FK_Visit_AspNetUsers_VisitMechanicId",
                table: "Visit");

            migrationBuilder.RenameColumn(
                name: "VisitMechanicId",
                table: "Visit",
                newName: "VisitMechanicID");

            migrationBuilder.RenameColumn(
                name: "VisitClientId",
                table: "Visit",
                newName: "VisitClientID");

            migrationBuilder.RenameIndex(
                name: "IX_Visit_VisitMechanicId",
                table: "Visit",
                newName: "IX_Visit_VisitMechanicID");

            migrationBuilder.RenameIndex(
                name: "IX_Visit_VisitClientId",
                table: "Visit",
                newName: "IX_Visit_VisitClientID");

            migrationBuilder.AddForeignKey(
                name: "FK_Visit_AspNetUsers_VisitClientID",
                table: "Visit",
                column: "VisitClientID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Visit_AspNetUsers_VisitMechanicID",
                table: "Visit",
                column: "VisitMechanicID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visit_AspNetUsers_VisitClientID",
                table: "Visit");

            migrationBuilder.DropForeignKey(
                name: "FK_Visit_AspNetUsers_VisitMechanicID",
                table: "Visit");

            migrationBuilder.RenameColumn(
                name: "VisitMechanicID",
                table: "Visit",
                newName: "VisitMechanicId");

            migrationBuilder.RenameColumn(
                name: "VisitClientID",
                table: "Visit",
                newName: "VisitClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Visit_VisitMechanicID",
                table: "Visit",
                newName: "IX_Visit_VisitMechanicId");

            migrationBuilder.RenameIndex(
                name: "IX_Visit_VisitClientID",
                table: "Visit",
                newName: "IX_Visit_VisitClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visit_AspNetUsers_VisitClientId",
                table: "Visit",
                column: "VisitClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Visit_AspNetUsers_VisitMechanicId",
                table: "Visit",
                column: "VisitMechanicId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
