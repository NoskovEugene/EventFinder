using Microsoft.EntityFrameworkCore.Migrations;

namespace EventFinder.Migrations
{
    public partial class AddrelationUserImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathToPhoto",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "User",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_User_ImageId",
                table: "User",
                column: "ImageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Image_ImageId",
                table: "User",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Image_ImageId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ImageId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "PathToPhoto",
                table: "User",
                type: "text",
                nullable: true);
        }
    }
}
