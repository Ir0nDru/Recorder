using Microsoft.EntityFrameworkCore.Migrations;

namespace Recorder.Service.Migrations
{
    public partial class AddCameraDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Cameras",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Cameras");
        }
    }
}
