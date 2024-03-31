using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace usue_online_tests.Migrations
{
    public partial class updateforeignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Presets_PresetId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Presets_Users_OwnerId",
                table: "Presets");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Presets",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PresetId",
                table: "Exams",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Presets_PresetId",
                table: "Exams",
                column: "PresetId",
                principalTable: "Presets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Presets_Users_OwnerId",
                table: "Presets",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Presets_PresetId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Presets_Users_OwnerId",
                table: "Presets");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Presets",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "PresetId",
                table: "Exams",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Presets_PresetId",
                table: "Exams",
                column: "PresetId",
                principalTable: "Presets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Presets_Users_OwnerId",
                table: "Presets",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
