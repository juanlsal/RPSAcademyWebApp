using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPSAcademy.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedOpponentsModell : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Opponents",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpponentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpponentInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpponentImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opponents", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Opponents");
        }
    }
}
