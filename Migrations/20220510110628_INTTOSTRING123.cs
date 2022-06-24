using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS_project.Migrations
{
    public partial class INTTOSTRING123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BUnitID",
                table: "onCheckpoint");

            migrationBuilder.AddColumn<string>(
                name: "Businessunit",
                table: "onCheckpoint",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Businessunit",
                table: "onCheckpoint");

            migrationBuilder.AddColumn<int>(
                name: "BUnitID",
                table: "onCheckpoint",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
