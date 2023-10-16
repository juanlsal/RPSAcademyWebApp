using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPSAcademy.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedSubjectIdToDefaultQuestionsForLinking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DefaultSubjectId",
                table: "DefaultQuestions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultSubjectId",
                table: "DefaultQuestions");
        }
    }
}
