using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace QuestionBank.DataManagement.Migrations
{
    /// <inheritdoc />
    public partial class QuestionAndRelatedTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionCategoryUserActions_QuestionCategory_QuestionCatego~",
                table: "QuestionCategoryUserActions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionCategoryUserActions_UserAccount_UserId",
                table: "QuestionCategoryUserActions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionCategoryUserActions",
                table: "QuestionCategoryUserActions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "QuestionCategory");

            migrationBuilder.RenameTable(
                name: "QuestionCategoryUserActions",
                newName: "QuestionCategoryUserAction");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionCategoryUserActions_UserId",
                table: "QuestionCategoryUserAction",
                newName: "IX_QuestionCategoryUserAction_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionCategoryUserActions_QuestionCategoryId",
                table: "QuestionCategoryUserAction",
                newName: "IX_QuestionCategoryUserAction_QuestionCategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "QuestionCategory",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "QuestionCategory",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionCategoryUserAction",
                table: "QuestionCategoryUserAction",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<int>(type: "integer", maxLength: 200, nullable: false),
                    Description = table.Column<int>(type: "integer", nullable: false),
                    SampleAnswer = table.Column<string>(type: "text", nullable: true),
                    QuestionCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    QuestionCategory = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkillsTag",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillsTag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionFeedBack",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    FeedBackSubmittedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FeedBackType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionFeedBack", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionFeedBack_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionFeedBack_UserAccount_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionFeedBackComment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CommentSubmittedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CommentStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionFeedBackComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionFeedBackComment_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionFeedBackComment_UserAccount_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionTag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TagId = table.Column<long>(type: "bigint", nullable: false),
                    QuestionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionTag_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionTag_SkillsTag_TagId",
                        column: x => x.TagId,
                        principalTable: "SkillsTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionFeedBack_QuestionId",
                table: "QuestionFeedBack",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionFeedBack_UserId",
                table: "QuestionFeedBack",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionFeedBackComment_QuestionId",
                table: "QuestionFeedBackComment",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionFeedBackComment_UserId",
                table: "QuestionFeedBackComment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTag_QuestionId",
                table: "QuestionTag",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTag_TagId",
                table: "QuestionTag",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionCategoryUserAction_QuestionCategory_QuestionCategor~",
                table: "QuestionCategoryUserAction",
                column: "QuestionCategoryId",
                principalTable: "QuestionCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionCategoryUserAction_UserAccount_UserId",
                table: "QuestionCategoryUserAction",
                column: "UserId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionCategoryUserAction_QuestionCategory_QuestionCategor~",
                table: "QuestionCategoryUserAction");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionCategoryUserAction_UserAccount_UserId",
                table: "QuestionCategoryUserAction");

            migrationBuilder.DropTable(
                name: "QuestionFeedBack");

            migrationBuilder.DropTable(
                name: "QuestionFeedBackComment");

            migrationBuilder.DropTable(
                name: "QuestionTag");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "SkillsTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionCategoryUserAction",
                table: "QuestionCategoryUserAction");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "QuestionCategory");

            migrationBuilder.RenameTable(
                name: "QuestionCategoryUserAction",
                newName: "QuestionCategoryUserActions");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionCategoryUserAction_UserId",
                table: "QuestionCategoryUserActions",
                newName: "IX_QuestionCategoryUserActions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionCategoryUserAction_QuestionCategoryId",
                table: "QuestionCategoryUserActions",
                newName: "IX_QuestionCategoryUserActions_QuestionCategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "QuestionCategory",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "QuestionCategory",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionCategoryUserActions",
                table: "QuestionCategoryUserActions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionCategoryUserActions_QuestionCategory_QuestionCatego~",
                table: "QuestionCategoryUserActions",
                column: "QuestionCategoryId",
                principalTable: "QuestionCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionCategoryUserActions_UserAccount_UserId",
                table: "QuestionCategoryUserActions",
                column: "UserId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
