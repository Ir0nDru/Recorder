using Microsoft.EntityFrameworkCore.Migrations;

namespace Recorder.Service.Migrations
{
    public partial class fixedColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CameraStatus",
                table: "Cameras");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Cameras",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Cameras");

            migrationBuilder.AddColumn<int>(
                name: "CameraStatus",
                table: "Cameras",
                nullable: false,
                defaultValue: 0);
        }
    }
}
