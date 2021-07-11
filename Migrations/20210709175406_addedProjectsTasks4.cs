using Microsoft.EntityFrameworkCore.Migrations;

namespace xZoneAPI.Migrations
{
    public partial class addedProjectsTasks4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "appTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SectionID",
                table: "appTasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_appTasks_SectionID",
                table: "appTasks",
                column: "SectionID");

            migrationBuilder.AddForeignKey(
                name: "FK_appTasks_Section_SectionID",
                table: "appTasks",
                column: "SectionID",
                principalTable: "Section",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appTasks_Section_SectionID",
                table: "appTasks");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropIndex(
                name: "IX_appTasks_SectionID",
                table: "appTasks");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "appTasks");

            migrationBuilder.DropColumn(
                name: "SectionID",
                table: "appTasks");
        }
    }
}
