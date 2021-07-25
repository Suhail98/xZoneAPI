using Microsoft.EntityFrameworkCore.Migrations;

namespace xZoneAPI.Migrations
{
    public partial class addedZoneAdminLocationAndZoneNumOfAdminLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminLocation",
                table: "Zones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumOfAdminLocation",
                table: "Zones",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminLocation",
                table: "Zones");

            migrationBuilder.DropColumn(
                name: "NumOfAdminLocation",
                table: "Zones");
        }
    }
}
