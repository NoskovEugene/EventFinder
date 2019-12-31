using Microsoft.EntityFrameworkCore.Migrations;

namespace EventFinder.Migrations
{
    public partial class fixForum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Forum",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Forum",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Forum_CategoryId",
                table: "Forum",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Forum_EventId",
                table: "Forum",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Forum_Category_CategoryId",
                table: "Forum",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Forum_Event_EventId",
                table: "Forum",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forum_Category_CategoryId",
                table: "Forum");

            migrationBuilder.DropForeignKey(
                name: "FK_Forum_Event_EventId",
                table: "Forum");

            migrationBuilder.DropIndex(
                name: "IX_Forum_CategoryId",
                table: "Forum");

            migrationBuilder.DropIndex(
                name: "IX_Forum_EventId",
                table: "Forum");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Forum");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Forum");
        }
    }
}
