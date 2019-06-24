using Microsoft.EntityFrameworkCore.Migrations;

namespace MyMaaser.data.Migrations
{
    public partial class GiveToMoneyUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "GiveToMoney",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GiveToMoney");
        }
    }
}
