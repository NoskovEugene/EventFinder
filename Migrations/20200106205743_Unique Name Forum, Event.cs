using Microsoft.EntityFrameworkCore.Migrations;

namespace EventFinder.Migrations
{
    public partial class UniqueNameForumEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Forum_Theme",
                table: "Forum",
                column: "Theme",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Event_Name",
                table: "Event",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Forum_Theme",
                table: "Forum");

            migrationBuilder.DropIndex(
                name: "IX_Event_Name",
                table: "Event");
        }
    }
}
