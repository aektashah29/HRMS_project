using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS_project.Migrations
{
    public partial class icollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_onCheckpoint_BUnitID",
                table: "onCheckpoint",
                column: "BUnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_onCheckpoint_bunits_BUnitID",
                table: "onCheckpoint",
                column: "BUnitID",
                principalTable: "bunits",
                principalColumn: "BUnitID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_onCheckpoint_bunits_BUnitID",
                table: "onCheckpoint");

            migrationBuilder.DropIndex(
                name: "IX_onCheckpoint_BUnitID",
                table: "onCheckpoint");
        }
    }
}
