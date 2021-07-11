using Microsoft.EntityFrameworkCore.Migrations;

namespace xZoneAPI.Migrations
{
    public partial class AddingManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Accounts_AccountId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_AccountId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Skills");

            migrationBuilder.AddColumn<int>(
                name: "RankID",
                table: "Accounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccountBadges",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    BadgeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountBadges", x => new { x.AccountID, x.BadgeID });
                    table.ForeignKey(
                        name: "FK_AccountBadges_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountBadges_Badges_BadgeID",
                        column: x => x.BadgeID,
                        principalTable: "Badges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RankID",
                table: "Accounts",
                column: "RankID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountBadges_BadgeID",
                table: "AccountBadges",
                column: "BadgeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Ranks_RankID",
                table: "Accounts",
                column: "RankID",
                principalTable: "Ranks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Ranks_RankID",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountBadges");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_RankID",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "RankID",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Skills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_AccountId",
                table: "Skills",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Accounts_AccountId",
                table: "Skills",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
