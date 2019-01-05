using Microsoft.EntityFrameworkCore.Migrations;

namespace Recorder.Service.Migrations
{
    public partial class AddRecordDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Records",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Records");
        }
    }
}
