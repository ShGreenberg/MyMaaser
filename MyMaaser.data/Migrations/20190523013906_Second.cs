using Microsoft.EntityFrameworkCore.Migrations;

namespace MyMaaser.data.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaaserGiven_Users_UserId",
                table: "MaaserGiven");

            migrationBuilder.DropForeignKey(
                name: "FK_MoneyEarned_Users_UserId",
                table: "MoneyEarned");

            migrationBuilder.CreateTable(
                name: "GiveToMoney",
                columns: table => new
                {
                    MaaserGivenId = table.Column<int>(nullable: false),
                    MoneyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiveToMoney", x => new { x.MoneyId, x.MaaserGivenId });
                    table.ForeignKey(
                        name: "FK_GiveToMoney_MaaserGiven_MaaserGivenId",
                        column: x => x.MaaserGivenId,
                        principalTable: "MaaserGiven",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GiveToMoney_MoneyEarned_MoneyId",
                        column: x => x.MoneyId,
                        principalTable: "MoneyEarned",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GiveToMoney_MaaserGivenId",
                table: "GiveToMoney",
                column: "MaaserGivenId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaaserGiven_Users_UserId",
                table: "MaaserGiven",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MoneyEarned_Users_UserId",
                table: "MoneyEarned",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaaserGiven_Users_UserId",
                table: "MaaserGiven");

            migrationBuilder.DropForeignKey(
                name: "FK_MoneyEarned_Users_UserId",
                table: "MoneyEarned");

            migrationBuilder.DropTable(
                name: "GiveToMoney");

            migrationBuilder.AddForeignKey(
                name: "FK_MaaserGiven_Users_UserId",
                table: "MaaserGiven",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoneyEarned_Users_UserId",
                table: "MoneyEarned",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
