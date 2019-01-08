using Microsoft.EntityFrameworkCore.Migrations;

namespace Recorder.Service.Migrations
{
    public partial class AddForeingKey2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_Cameras_CameraId",
                table: "Records");

            migrationBuilder.AlterColumn<int>(
                name: "CameraId",
                table: "Records",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Cameras_CameraId",
                table: "Records",
                column: "CameraId",
                principalTable: "Cameras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_Cameras_CameraId",
                table: "Records");

            migrationBuilder.AlterColumn<int>(
                name: "CameraId",
                table: "Records",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Cameras_CameraId",
                table: "Records",
                column: "CameraId",
                principalTable: "Cameras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
