using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPSAcademy.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedMorePropertiesToDefaultSubjectcsModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "DefaultSubjects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "DefaultSubjects",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "DefaultSubjects");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DefaultSubjects");
        }
    }
}
