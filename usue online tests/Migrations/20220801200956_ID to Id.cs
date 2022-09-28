using Microsoft.EntityFrameworkCore.Migrations;

namespace usue_online_tests.Migrations
{
    public partial class IDtoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presets_Users_OwnerID",
                table: "Presets");

            migrationBuilder.DropForeignKey(
                name: "FK_UserExamResults_Users_UserID",
                table: "UserExamResults");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "UserExamResults",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserExamResults_UserID",
                table: "UserExamResults",
                newName: "IX_UserExamResults_UserId");

            migrationBuilder.RenameColumn(
                name: "OwnerID",
                table: "Presets",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Presets_OwnerID",
                table: "Presets",
                newName: "IX_Presets_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Presets_Users_OwnerId",
                table: "Presets",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserExamResults_Users_UserId",
                table: "UserExamResults",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presets_Users_OwnerId",
                table: "Presets");

            migrationBuilder.DropForeignKey(
                name: "FK_UserExamResults_Users_UserId",
                table: "UserExamResults");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserExamResults",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_UserExamResults_UserId",
                table: "UserExamResults",
                newName: "IX_UserExamResults_UserID");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Presets",
                newName: "OwnerID");

            migrationBuilder.RenameIndex(
                name: "IX_Presets_OwnerId",
                table: "Presets",
                newName: "IX_Presets_OwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Presets_Users_OwnerID",
                table: "Presets",
                column: "OwnerID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserExamResults_Users_UserID",
                table: "UserExamResults",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
