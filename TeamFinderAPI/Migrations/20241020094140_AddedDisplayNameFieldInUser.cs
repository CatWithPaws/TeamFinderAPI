using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamFinderAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedDisplayNameFieldInUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DispayName",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DispayName",
                table: "User");
        }
    }
}
