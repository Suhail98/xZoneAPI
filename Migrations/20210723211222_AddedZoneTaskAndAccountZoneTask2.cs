using Microsoft.EntityFrameworkCore.Migrations;

namespace xZoneAPI.Migrations
{
    public partial class AddedZoneTaskAndAccountZoneTask2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Accounts_UserId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "ZoneTasks");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_UserId",
                table: "ZoneTasks",
                newName: "IX_ZoneTasks_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZoneTasks",
                table: "ZoneTasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneTasks_Accounts_UserId",
                table: "ZoneTasks",
                column: "UserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZoneTasks_Accounts_UserId",
                table: "ZoneTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ZoneTasks",
                table: "ZoneTasks");

            migrationBuilder.RenameTable(
                name: "ZoneTasks",
                newName: "Tasks");

            migrationBuilder.RenameIndex(
                name: "IX_ZoneTasks_UserId",
                table: "Tasks",
                newName: "IX_Tasks_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Accounts_UserId",
                table: "Tasks",
                column: "UserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
