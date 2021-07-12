using Microsoft.EntityFrameworkCore.Migrations;

namespace xZoneAPI.Migrations
{
    public partial class removedRankForienKeyFromAccount69 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Ranks_RankID",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_RankID",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "RankID",
                table: "Accounts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RankID",
                table: "Accounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RankID",
                table: "Accounts",
                column: "RankID");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Ranks_RankID",
                table: "Accounts",
                column: "RankID",
                principalTable: "Ranks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
