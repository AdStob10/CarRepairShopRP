using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRepairShopRP.Migrations
{
    public partial class image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OldPartImageImageModelId",
                table: "ReplacedPart",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageModelId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReplacedPart_OldPartImageImageModelId",
                table: "ReplacedPart",
                column: "OldPartImageImageModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReplacedPart_Images_OldPartImageImageModelId",
                table: "ReplacedPart",
                column: "OldPartImageImageModelId",
                principalTable: "Images",
                principalColumn: "ImageModelId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReplacedPart_Images_OldPartImageImageModelId",
                table: "ReplacedPart");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_ReplacedPart_OldPartImageImageModelId",
                table: "ReplacedPart");

            migrationBuilder.DropColumn(
                name: "OldPartImageImageModelId",
                table: "ReplacedPart");
        }
    }
}
