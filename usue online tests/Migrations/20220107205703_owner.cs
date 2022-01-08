using Microsoft.EntityFrameworkCore.Migrations;

namespace usue_online_tests.Migrations
{
    public partial class owner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerID",
                table: "Presets",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Presets_OwnerID",
                table: "Presets",
                column: "OwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Presets_Users_OwnerID",
                table: "Presets",
                column: "OwnerID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presets_Users_OwnerID",
                table: "Presets");

            migrationBuilder.DropIndex(
                name: "IX_Presets_OwnerID",
                table: "Presets");

            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Presets");
        }
    }
}
