using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPSAcademy.Data.Migrations
{
    /// <inheritdoc />
    public partial class ImprovedOpponentsModelPropertyNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OpponentName",
                table: "Opponents",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "OpponentInfo",
                table: "Opponents",
                newName: "Info");

            migrationBuilder.RenameColumn(
                name: "OpponentImageUrl",
                table: "Opponents",
                newName: "ImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Opponents",
                newName: "OpponentName");

            migrationBuilder.RenameColumn(
                name: "Info",
                table: "Opponents",
                newName: "OpponentInfo");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Opponents",
                newName: "OpponentImageUrl");
        }
    }
}
