using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace usue_online_tests.Migrations
{
    public partial class updateforeignkey2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserExamResults_Exams_ExamId",
                table: "UserExamResults");

            migrationBuilder.DropForeignKey(
                name: "FK_UserExamResults_Users_UserId",
                table: "UserExamResults");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserExamResults",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ExamId",
                table: "UserExamResults",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserExamResults_Exams_ExamId",
                table: "UserExamResults",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserExamResults_Users_UserId",
                table: "UserExamResults",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserExamResults_Exams_ExamId",
                table: "UserExamResults");

            migrationBuilder.DropForeignKey(
                name: "FK_UserExamResults_Users_UserId",
                table: "UserExamResults");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserExamResults",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ExamId",
                table: "UserExamResults",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_UserExamResults_Exams_ExamId",
                table: "UserExamResults",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserExamResults_Users_UserId",
                table: "UserExamResults",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
