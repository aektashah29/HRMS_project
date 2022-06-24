using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS_project.Migrations
{
    public partial class deptinstring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeptId",
                table: "onCheckpoint");

            migrationBuilder.AddColumn<string>(
                name: "DeptName",
                table: "onCheckpoint",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeptName",
                table: "onCheckpoint");

            migrationBuilder.AddColumn<int>(
                name: "DeptId",
                table: "onCheckpoint",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
