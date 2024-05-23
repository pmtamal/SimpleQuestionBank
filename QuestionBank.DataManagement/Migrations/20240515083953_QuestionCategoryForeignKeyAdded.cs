using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuestionBank.DataManagement.Migrations
{
    /// <inheritdoc />
    public partial class QuestionCategoryForeignKeyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionCategory",
                table: "Question");

            migrationBuilder.CreateIndex(
                name: "IX_Question_QuestionCategoryId",
                table: "Question",
                column: "QuestionCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_QuestionCategory_QuestionCategoryId",
                table: "Question",
                column: "QuestionCategoryId",
                principalTable: "QuestionCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_QuestionCategory_QuestionCategoryId",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Question_QuestionCategoryId",
                table: "Question");

            migrationBuilder.AddColumn<long>(
                name: "QuestionCategory",
                table: "Question",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
