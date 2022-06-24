using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS_project.Migrations
{
    public partial class bunitstring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BUnitID",
                table: "offCheckpoint");

            migrationBuilder.AddColumn<string>(
                name: "Businessunit",
                table: "offCheckpoint",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Businessunit",
                table: "offCheckpoint");

            migrationBuilder.AddColumn<int>(
                name: "BUnitID",
                table: "offCheckpoint",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
