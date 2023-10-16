using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPSAcademy.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserInfoEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfo_AspNetUsers_UserId",
                table: "UserInfo");

            migrationBuilder.DropIndex(
                name: "IX_UserInfo_UserId",
                table: "UserInfo");

            migrationBuilder.RenameColumn(
                name: "ProfileUrl",
                table: "UserInfo",
                newName: "ProfileImageUrl");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfileImageUrl",
                table: "UserInfo",
                newName: "ProfileUrl");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserInfo",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_UserId",
                table: "UserInfo",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfo_AspNetUsers_UserId",
                table: "UserInfo",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
