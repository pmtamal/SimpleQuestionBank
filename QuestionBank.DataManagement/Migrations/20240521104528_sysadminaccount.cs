using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuestionBank.DataManagement.Migrations
{
    /// <inheritdoc />
    public partial class sysadminaccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"WITH person_insert AS (
    INSERT INTO public.""Person""(""FirstName"", ""MiddleName"", ""LastName"", ""Address"", ""DOB"")
	VALUES ('QuestionBank', '', 'admin', 'N/A', '2000-05-01')
    RETURNING ""Id""
)
-- Use the returned id to insert into the child table
INSERT INTO public.""UserAccount"" (
	""Email"", ""Password"", ""CellNo"", ""RegisteredOn"", ""PasswordExpiredOn"", ""AccountStatus"", ""PersonId"", ""RoleId"")
SELECT 'sysadmin@dsinnovators.com','$2a$11$ovxD92Cle7u7YzYanDxn5OJcUB0YE3LEI5zdCN3HxdvWjl54It5HG','01711485698','2024-05-21 07:01:29.162694','2024-05-21 07:01:29.162694',1,""Id"",1 
FROM person_insert

COMMIT;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
