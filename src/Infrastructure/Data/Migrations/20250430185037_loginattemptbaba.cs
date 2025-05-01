using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class loginattemptbaba : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Önce foreign key’i kaldır
            migrationBuilder.DropForeignKey(
                name: "FK_LoginAttempts_Admins_AdminId",
                table: "LoginAttempts");

            // Sonra index varsa kaldır (opsiyonel ama genelde otomatik oluşur)
            migrationBuilder.DropIndex(
                name: "IX_LoginAttempts_AdminId",
                table: "LoginAttempts");

            // En son kolonu sil
            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "LoginAttempts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Geri alma durumunda kolon tekrar ekleniyor
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
