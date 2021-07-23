using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace xZoneAPI.Migrations
{
    public partial class AddedZoneTaskAndAccountZoneTask3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZoneTasks_Accounts_UserId",
                table: "ZoneTasks");

            migrationBuilder.DropColumn(
                name: "CompleteDate",
                table: "ZoneTasks");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ZoneTasks",
                newName: "ZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_ZoneTasks_UserId",
                table: "ZoneTasks",
                newName: "IX_ZoneTasks_ZoneId");

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CompleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: true),
                    parentID = table.Column<int>(type: "int", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remainder = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Accounts_UserId",
                        column: x => x.UserId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountZoneTasks_ZoneTaskID",
                table: "AccountZoneTasks",
                column: "ZoneTaskID");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountZoneTasks_ZoneTasks_ZoneTaskID",
                table: "AccountZoneTasks",
                column: "ZoneTaskID",
                principalTable: "ZoneTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneTasks_Zones_ZoneId",
                table: "ZoneTasks",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountZoneTasks_ZoneTasks_ZoneTaskID",
                table: "AccountZoneTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_ZoneTasks_Zones_ZoneId",
                table: "ZoneTasks");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_AccountZoneTasks_ZoneTaskID",
                table: "AccountZoneTasks");

            migrationBuilder.RenameColumn(
                name: "ZoneId",
                table: "ZoneTasks",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ZoneTasks_ZoneId",
                table: "ZoneTasks",
                newName: "IX_ZoneTasks_UserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CompleteDate",
                table: "ZoneTasks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneTasks_Accounts_UserId",
                table: "ZoneTasks",
                column: "UserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
