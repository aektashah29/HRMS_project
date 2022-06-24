using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS_project.Migrations
{
    public partial class offasignee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "offCheckpoint");

            migrationBuilder.AddColumn<string>(
                name: "AssigneeName",
                table: "offCheckpoint",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssigneeName",
                table: "offCheckpoint");

            migrationBuilder.AddColumn<int>(
                name: "AssigneeId",
                table: "offCheckpoint",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
