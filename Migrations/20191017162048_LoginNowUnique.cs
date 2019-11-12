using Microsoft.EntityFrameworkCore.Migrations;

namespace EventFinder.Migrations
{
    public partial class LoginNowUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_User_Login",
                table: "User",
                column: "Login",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_Login",
                table: "User");
        }
    }
}
