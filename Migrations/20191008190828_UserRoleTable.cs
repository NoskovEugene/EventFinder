using Microsoft.EntityFrameworkCore.Migrations;
using EventFinder.Models.Entity;
using EventFinder.Extensions;
using System.Linq;
using EventFinder.Models.Enums;

namespace EventFinder.Migrations
{
    public partial class UserRoleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            EnumExtensions.GetValues<RoleEnum>().ToList().ForEach(v=>{
                try{
                    migrationBuilder.InsertData
                    (
                        nameof(Role),
                        new[]{
                            nameof(Role.Id),
                            nameof(Role.RoleName)
                        },
                        new object[]{
                            (int)v,
                            v.GetDisplayName()
                        }
                    );
                }
                catch{
                    migrationBuilder.UpdateData
                    (
                        table:nameof(Role),
                        keyColumn:nameof(Role.Id),
                        keyValue: (int)v,
                        column: nameof(Role.RoleName),
                        value:v.GetDisplayName()
                    );
                }
            });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRole");
        }
    }
}
