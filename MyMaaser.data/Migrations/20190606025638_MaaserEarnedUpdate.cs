using Microsoft.EntityFrameworkCore.Migrations;

namespace MyMaaser.data.Migrations
{
    public partial class MaaserEarnedUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "From",
                table: "MoneyEarned",
                newName: "RecievedFrom");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecievedFrom",
                table: "MoneyEarned",
                newName: "From");
        }
    }
}
