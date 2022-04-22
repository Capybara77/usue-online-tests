using Microsoft.EntityFrameworkCore.Migrations;

namespace usue_online_tests.Migrations
{
    public partial class examResult2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamTestAnswer_UserExamResults_UserExamResultId",
                table: "ExamTestAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamTestAnswer",
                table: "ExamTestAnswer");

            migrationBuilder.RenameTable(
                name: "ExamTestAnswer",
                newName: "ExamTestAnswers");

            migrationBuilder.RenameIndex(
                name: "IX_ExamTestAnswer_UserExamResultId",
                table: "ExamTestAnswers",
                newName: "IX_ExamTestAnswers_UserExamResultId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamTestAnswers",
                table: "ExamTestAnswers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamTestAnswers_UserExamResults_UserExamResultId",
                table: "ExamTestAnswers",
                column: "UserExamResultId",
                principalTable: "UserExamResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamTestAnswers_UserExamResults_UserExamResultId",
                table: "ExamTestAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamTestAnswers",
                table: "ExamTestAnswers");

            migrationBuilder.RenameTable(
                name: "ExamTestAnswers",
                newName: "ExamTestAnswer");

            migrationBuilder.RenameIndex(
                name: "IX_ExamTestAnswers_UserExamResultId",
                table: "ExamTestAnswer",
                newName: "IX_ExamTestAnswer_UserExamResultId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamTestAnswer",
                table: "ExamTestAnswer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamTestAnswer_UserExamResults_UserExamResultId",
                table: "ExamTestAnswer",
                column: "UserExamResultId",
                principalTable: "UserExamResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
