using Microsoft.EntityFrameworkCore.Migrations;

namespace DiplomToyStore.Migrations
{
    public partial class MergetblProductandphoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblProductPhotos");

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "tblProducts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "tblProducts");

            migrationBuilder.CreateTable(
                name: "tblProductPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProductPhotos_tblProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tblProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblProductPhotos_ProductId",
                table: "tblProductPhotos",
                column: "ProductId");
        }
    }
}
