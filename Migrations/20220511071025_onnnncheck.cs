using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS_project.Migrations
{
    public partial class onnnncheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssigneeName",
                table: "onCheckpoint");

            migrationBuilder.DropColumn(
                name: "Businessunit",
                table: "onCheckpoint");

            migrationBuilder.DropColumn(
                name: "DeptName",
                table: "onCheckpoint");

            migrationBuilder.AddColumn<int>(
                name: "AssigneeId",
                table: "onCheckpoint",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BUnitID",
                table: "onCheckpoint",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeptId",
                table: "onCheckpoint",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "onCheckpoint");

            migrationBuilder.DropColumn(
                name: "BUnitID",
                table: "onCheckpoint");

            migrationBuilder.DropColumn(
                name: "DeptId",
                table: "onCheckpoint");

            migrationBuilder.AddColumn<string>(
                name: "AssigneeName",
                table: "onCheckpoint",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Businessunit",
                table: "onCheckpoint",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeptName",
                table: "onCheckpoint",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
