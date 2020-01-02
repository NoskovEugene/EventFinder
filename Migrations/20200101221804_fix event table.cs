using Microsoft.EntityFrameworkCore.Migrations;

namespace EventFinder.Migrations
{
    public partial class fixeventtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventLink",
                table: "Event",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventLink",
                table: "Event");
        }
    }
}
