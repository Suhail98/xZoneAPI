using Microsoft.EntityFrameworkCore.Migrations;

namespace xZoneAPI.Migrations
{
    public partial class temp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountSkill_Accounts_AccountID",
                table: "AccountSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountSkill_Skills_SkillID",
                table: "AccountSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountSkill",
                table: "AccountSkill");

            migrationBuilder.RenameTable(
                name: "AccountSkill",
                newName: "AccountSkills");

            migrationBuilder.RenameIndex(
                name: "IX_AccountSkill_SkillID",
                table: "AccountSkills",
                newName: "IX_AccountSkills_SkillID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountSkills",
                table: "AccountSkills",
                columns: new[] { "AccountID", "SkillID" });

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSkills_Accounts_AccountID",
                table: "AccountSkills",
                column: "AccountID",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSkills_Skills_SkillID",
                table: "AccountSkills",
                column: "SkillID",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountSkills_Accounts_AccountID",
                table: "AccountSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountSkills_Skills_SkillID",
                table: "AccountSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountSkills",
                table: "AccountSkills");

            migrationBuilder.RenameTable(
                name: "AccountSkills",
                newName: "AccountSkill");

            migrationBuilder.RenameIndex(
                name: "IX_AccountSkills_SkillID",
                table: "AccountSkill",
                newName: "IX_AccountSkill_SkillID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountSkill",
                table: "AccountSkill",
                columns: new[] { "AccountID", "SkillID" });

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSkill_Accounts_AccountID",
                table: "AccountSkill",
                column: "AccountID",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSkill_Skills_SkillID",
                table: "AccountSkill",
                column: "SkillID",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
