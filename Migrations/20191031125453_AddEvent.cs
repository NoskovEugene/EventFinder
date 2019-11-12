using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EventFinder.Migrations
{
    public partial class AddEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    EventDate = table.Column<DateTime>(nullable: false),
                    Leader = table.Column<string>(nullable: true),
                    Place = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_OwnerId",
                table: "Event",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");
        }
    }
}
