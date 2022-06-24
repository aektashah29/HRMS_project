using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS_project.Migrations
{
    public partial class asigneeinstring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "onCheckpoint");

            migrationBuilder.AddColumn<string>(
                name: "AssigneeName",
                table: "onCheckpoint",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssigneeName",
                table: "onCheckpoint");

            migrationBuilder.AddColumn<int>(
                name: "AssigneeId",
                table: "onCheckpoint",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
