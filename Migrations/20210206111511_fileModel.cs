using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRepairShopRP.Migrations
{
    public partial class fileModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReplacedPart_Images_OldPartImageImageModelId",
                table: "ReplacedPart");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.RenameColumn(
                name: "OldPartImageImageModelId",
                table: "ReplacedPart",
                newName: "OldPartImageFileModelId");

            migrationBuilder.RenameIndex(
                name: "IX_ReplacedPart_OldPartImageImageModelId",
                table: "ReplacedPart",
                newName: "IX_ReplacedPart_OldPartImageFileModelId");

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    FileModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.FileModelId);
                });

     
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReplacedPart_Files_OldPartImageFileModelId",
                table: "ReplacedPart");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.RenameColumn(
                name: "OldPartImageFileModelId",
                table: "ReplacedPart",
                newName: "OldPartImageImageModelId");

            migrationBuilder.RenameIndex(
                name: "IX_ReplacedPart_OldPartImageFileModelId",
                table: "ReplacedPart",
                newName: "IX_ReplacedPart_OldPartImageImageModelId");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageModelId);
                });


        }
    }
}
