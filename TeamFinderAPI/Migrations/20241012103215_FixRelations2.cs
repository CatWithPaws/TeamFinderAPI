using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamFinderAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixRelations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_CreatedById",
                table: "Post");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Post",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Post_CreatedById",
                table: "Post",
                newName: "IX_Post_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_User_UserId",
                table: "Post",
                column: "UserId",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_UserId",
                table: "Post");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Post",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Post_UserId",
                table: "Post",
                newName: "IX_Post_CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_User_CreatedById",
                table: "Post",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
