using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS_project.Migrations
{
    public partial class isrequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRequired",
                table: "onCheckpoint");

            migrationBuilder.DropColumn(
                name: "IsRequired",
                table: "offCheckpoint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRequired",
                table: "onCheckpoint",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRequired",
                table: "offCheckpoint",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
