using Microsoft.EntityFrameworkCore.Migrations;

namespace xZoneAPI.Migrations
{
    public partial class roadmapupdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Roadmaps",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Roadmaps");
        }
    }
}
