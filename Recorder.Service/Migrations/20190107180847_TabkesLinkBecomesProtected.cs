using Microsoft.EntityFrameworkCore.Migrations;

namespace Recorder.Service.Migrations
{
    public partial class TabkesLinkBecomesProtected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_Cameras_CameraId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_CameraId",
                table: "Records");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Records_CameraId",
                table: "Records",
                column: "CameraId");

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
