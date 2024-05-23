using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuestionBank.DataManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddTestCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"insert into ""SkillsTag""(""Name"",""Description"") Values ('ProblemSolving','ProblemSolving'),('CriticalThinking','CriticalThinking'),('Programming','Programming'),('ProjectManagement','ProjectManagement')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
