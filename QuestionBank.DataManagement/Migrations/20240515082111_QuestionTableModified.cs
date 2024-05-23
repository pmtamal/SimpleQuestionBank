using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuestionBank.DataManagement.Migrations
{
    /// <inheritdoc />
    public partial class QuestionTableModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "QuestionFeedBackComment",
                type: "character varying(5000)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Question",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Question_UserId",
                table: "Question",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_UserAccount_UserId",
                table: "Question",
                column: "UserId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_UserAccount_UserId",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Question_UserId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "QuestionFeedBackComment");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Question");
        }
    }
}
