using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuestionBank.DataManagement.Migrations
{
    /// <inheritdoc />
    public partial class QuestioFeedBackRelationAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FeedBackSubmittedOn",
                table: "QuestionFeedBack",
                newName: "LastFeedBackSubmittedOn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastFeedBackSubmittedOn",
                table: "QuestionFeedBack",
                newName: "FeedBackSubmittedOn");
        }
    }
}
