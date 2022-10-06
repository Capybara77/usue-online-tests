using Microsoft.EntityFrameworkCore.Migrations;

namespace usue_online_tests.Migrations
{
    public partial class deletemintopass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinutesToPass",
                table: "Presets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinutesToPass",
                table: "Presets",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
