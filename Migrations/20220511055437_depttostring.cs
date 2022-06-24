using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS_project.Migrations
{
    public partial class depttostring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeptId",
                table: "offCheckpoint");

            migrationBuilder.AddColumn<string>(
                name: "DeptName",
                table: "offCheckpoint",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeptName",
                table: "offCheckpoint");

            migrationBuilder.AddColumn<int>(
                name: "DeptId",
                table: "offCheckpoint",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
