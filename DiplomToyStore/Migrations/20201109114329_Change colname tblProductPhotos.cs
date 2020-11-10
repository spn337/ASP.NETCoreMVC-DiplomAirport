using Microsoft.EntityFrameworkCore.Migrations;

namespace DiplomToyStore.Migrations
{
    public partial class ChangecolnametblProductPhotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "tblProductPhotos");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "tblProductPhotos",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "tblProductPhotos");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "tblProductPhotos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
