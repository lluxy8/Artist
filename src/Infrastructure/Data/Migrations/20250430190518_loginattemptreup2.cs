using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class loginattemptreup2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginAttempts_Admins_AdminId",
                table: "LoginAttempts");

            migrationBuilder.DropIndex(
                name: "IX_LoginAttempts_AdminId",
                table: "LoginAttempts");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "LoginAttempts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AdminId",
                table: "LoginAttempts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoginAttempts_AdminId",
                table: "LoginAttempts",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoginAttempts_Admins_AdminId",
                table: "LoginAttempts",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id");
        }
    }
}
