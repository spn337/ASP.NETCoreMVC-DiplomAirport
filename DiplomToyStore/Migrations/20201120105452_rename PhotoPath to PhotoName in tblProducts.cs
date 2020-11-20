using Microsoft.EntityFrameworkCore.Migrations;

namespace DiplomToyStore.Migrations
{
    public partial class renamePhotoPathtoPhotoNameintblProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "tblProducts");

            migrationBuilder.AddColumn<string>(
                name: "PhotoName",
                table: "tblProducts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoName",
                table: "tblProducts");

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "tblProducts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
