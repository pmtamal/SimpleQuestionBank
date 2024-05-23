using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuestionBank.DataManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddOtherRolesExceptAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"insert into ""Role""(""Name"",""Description"") Values ('reviewer','Review question posted by question creator'),('approver','Permission to merge question'),('question_maker','Post question in the system')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
