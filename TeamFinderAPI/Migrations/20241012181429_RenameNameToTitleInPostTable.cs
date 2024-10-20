using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamFinderAPI.Migrations
{
    /// <inheritdoc />
    public partial class RenameNameToTitleInPostTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Post",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Discord",
                table: "Post",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TelegramLink",
                table: "Post",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discord",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "TelegramLink",
                table: "Post");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Post",
                newName: "Name");
        }
    }
}
