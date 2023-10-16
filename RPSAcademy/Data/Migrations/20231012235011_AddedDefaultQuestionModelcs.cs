using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPSAcademy.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDefaultQuestionModelcs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DefaultQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnswerA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnswerB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnswerC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnswerD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectAnswer = table.Column<int>(type: "int", nullable: false),
                    CorrectAnswerText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultQuestions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefaultQuestions");
        }
    }
}
