using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EventFinder.Migrations
{
    public partial class AddForum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Forum",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forum", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forum_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForumMessage_ForumId",
                table: "ForumMessage",
                column: "ForumId");

            migrationBuilder.CreateIndex(
                name: "IX_Forum_OwnerId",
                table: "Forum",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumMessage_Forum_ForumId",
                table: "ForumMessage",
                column: "ForumId",
                principalTable: "Forum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumMessage_Forum_ForumId",
                table: "ForumMessage");

            migrationBuilder.DropTable(
                name: "Forum");

            migrationBuilder.DropIndex(
                name: "IX_ForumMessage_ForumId",
                table: "ForumMessage");
        }
    }
}
