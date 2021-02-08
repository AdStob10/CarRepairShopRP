using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRepairShopRP.Migrations
{
    public partial class invoiceUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IssuedToId",
                table: "Invoice",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_IssuedToId",
                table: "Invoice",
                column: "IssuedToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_AspNetUsers_IssuedToId",
                table: "Invoice",
                column: "IssuedToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_AspNetUsers_IssuedToId",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_IssuedToId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "IssuedToId",
                table: "Invoice");
        }
    }
}
