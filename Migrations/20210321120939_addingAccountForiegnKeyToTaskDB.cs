using Microsoft.EntityFrameworkCore.Migrations;

namespace xZoneAPI.Migrations
{
    public partial class addingAccountForiegnKeyToTaskDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_appTasks_UserId",
                table: "appTasks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_appTasks_Accounts_UserId",
                table: "appTasks",
                column: "UserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appTasks_Accounts_UserId",
                table: "appTasks");

            migrationBuilder.DropIndex(
                name: "IX_appTasks_UserId",
                table: "appTasks");
        }
    }
}
