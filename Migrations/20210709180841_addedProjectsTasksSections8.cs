using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace xZoneAPI.Migrations
{
    public partial class addedProjectsTasksSections8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appTasks");

            migrationBuilder.AddColumn<int>(
                name: "OwnerID",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_OwnerID",
                table: "Projects",
                column: "OwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Accounts_OwnerID",
                table: "Projects",
                column: "OwnerID",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Accounts_OwnerID",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_OwnerID",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Projects");

            migrationBuilder.CreateTable(
                name: "appTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: true),
                    Remainder = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    parentID = table.Column<int>(type: "int", nullable: true),
                    SectionID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_appTasks_appTasks_parentID",
                        column: x => x.parentID,
                        principalTable: "appTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_appTasks_Sections_SectionID",
                        column: x => x.SectionID,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_appTasks_parentID",
                table: "appTasks",
                column: "parentID");

            migrationBuilder.CreateIndex(
                name: "IX_appTasks_SectionID",
                table: "appTasks",
                column: "SectionID");
        }
    }
}
