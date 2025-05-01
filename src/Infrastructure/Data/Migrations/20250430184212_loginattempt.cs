using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class loginattempt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginAttempts_Admins_AdminId",
                table: "LoginAttempts");

            migrationBuilder.AddForeignKey(
                name: "FK_LoginAttempts_Admins_AdminId",
                table: "LoginAttempts",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginAttempts_Admins_AdminId",
                table: "LoginAttempts");

            migrationBuilder.AddForeignKey(
                name: "FK_LoginAttempts_Admins_AdminId",
                table: "LoginAttempts",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
