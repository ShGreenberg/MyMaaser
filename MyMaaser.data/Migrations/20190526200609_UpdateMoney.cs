using Microsoft.EntityFrameworkCore.Migrations;

namespace MyMaaser.data.Migrations
{
    public partial class UpdateMoney : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AmountLeft",
                table: "MoneyEarned",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "PaidUp",
                table: "MoneyEarned",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountLeft",
                table: "MoneyEarned");

            migrationBuilder.DropColumn(
                name: "PaidUp",
                table: "MoneyEarned");
        }
    }
}
