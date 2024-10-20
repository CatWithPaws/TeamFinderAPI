using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamFinderAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangedUserPostsRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_UserID",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_UserID",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Post");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Post",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Post_UserID",
                table: "Post",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_User_UserID",
                table: "Post",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID");
        }
    }
}
