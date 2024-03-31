using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace usue_online_tests.Migrations
{
    public partial class updateforeignkey3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamTestAnswers_UserExamResults_UserExamResultId",
                table: "ExamTestAnswers");

            migrationBuilder.AlterColumn<int>(
                name: "UserExamResultId",
                table: "ExamTestAnswers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamTestAnswers_UserExamResults_UserExamResultId",
                table: "ExamTestAnswers",
                column: "UserExamResultId",
                principalTable: "UserExamResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamTestAnswers_UserExamResults_UserExamResultId",
                table: "ExamTestAnswers");

            migrationBuilder.AlterColumn<int>(
                name: "UserExamResultId",
                table: "ExamTestAnswers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamTestAnswers_UserExamResults_UserExamResultId",
                table: "ExamTestAnswers",
                column: "UserExamResultId",
                principalTable: "UserExamResults",
                principalColumn: "Id");
        }
    }
}
