using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRepairShopRP.Migrations
{
    public partial class newpartbill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NewPartBillFileModelId",
                table: "ReplacedPart",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReplacedPart_NewPartBillFileModelId",
                table: "ReplacedPart",
                column: "NewPartBillFileModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReplacedPart_Files_NewPartBillFileModelId",
                table: "ReplacedPart",
                column: "NewPartBillFileModelId",
                principalTable: "Files",
                principalColumn: "FileModelId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReplacedPart_Files_NewPartBillFileModelId",
                table: "ReplacedPart");

            migrationBuilder.DropIndex(
                name: "IX_ReplacedPart_NewPartBillFileModelId",
                table: "ReplacedPart");

            migrationBuilder.DropColumn(
                name: "NewPartBillFileModelId",
                table: "ReplacedPart");
        }
    }
}
