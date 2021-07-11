using Microsoft.EntityFrameworkCore.Migrations;

namespace xZoneAPI.Migrations
{
    public partial class AddManyToManyRelationshipBetweenSkillAndAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Skills",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bio",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccountSkill",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    SkillID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSkill", x => new { x.AccountID, x.SkillID });
                    table.ForeignKey(
                        name: "FK_AccountSkill_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountSkill_Skills_SkillID",
                        column: x => x.SkillID,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_AccountId",
                table: "Skills",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSkill_SkillID",
                table: "AccountSkill",
                column: "SkillID");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Accounts_AccountId",
                table: "Skills",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Accounts_AccountId",
                table: "Skills");

            migrationBuilder.DropTable(
                name: "AccountSkill");

            migrationBuilder.DropIndex(
                name: "IX_Skills_AccountId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "bio",
                table: "Accounts");
        }
    }
}
