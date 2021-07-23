using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace xZoneAPI.Migrations
{
    public partial class AddedZoneTaskAndAccountZoneTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountZoneTasks",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    ZoneTaskID = table.Column<int>(type: "int", nullable: false),
                    CompleteDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountZoneTasks", x => new { x.AccountID, x.ZoneTaskID });
                    table.ForeignKey(
                        name: "FK_AccountZoneTasks_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountZoneTasks");
        }
    }
}
