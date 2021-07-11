using Microsoft.EntityFrameworkCore.Migrations;

namespace xZoneAPI.Migrations
{
    public partial class addedProjectsTasksSections6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Projects_ProjectId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Sections_ProjectId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Sections");

            migrationBuilder.AddColumn<int>(
                name: "ParentProjectID",
                table: "Sections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sections_ParentProjectID",
                table: "Sections",
                column: "ParentProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Projects_ParentProjectID",
                table: "Sections",
                column: "ParentProjectID",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Projects_ParentProjectID",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Sections_ParentProjectID",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "ParentProjectID",
                table: "Sections");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Sections",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sections_ProjectId",
                table: "Sections",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Projects_ProjectId",
                table: "Sections",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
