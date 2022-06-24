using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS_project.Migrations
{
    public partial class offcheckpintchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssigneeName",
                table: "offCheckpoint");

            migrationBuilder.DropColumn(
                name: "Businessunit",
                table: "offCheckpoint");

            migrationBuilder.DropColumn(
                name: "DeptName",
                table: "offCheckpoint");

            migrationBuilder.AddColumn<int>(
                name: "AssigneeId",
                table: "offCheckpoint",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BUnitID",
                table: "offCheckpoint",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeptId",
                table: "offCheckpoint",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "offCheckpoint");

            migrationBuilder.DropColumn(
                name: "BUnitID",
                table: "offCheckpoint");

            migrationBuilder.DropColumn(
                name: "DeptId",
                table: "offCheckpoint");

            migrationBuilder.AddColumn<string>(
                name: "AssigneeName",
                table: "offCheckpoint",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Businessunit",
                table: "offCheckpoint",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeptName",
                table: "offCheckpoint",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
