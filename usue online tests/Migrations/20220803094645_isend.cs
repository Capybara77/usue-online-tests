using Microsoft.EntityFrameworkCore.Migrations;

namespace usue_online_tests.Migrations
{
    public partial class isend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEnd",
                table: "Exams",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnd",
                table: "Exams");
        }
    }
}
