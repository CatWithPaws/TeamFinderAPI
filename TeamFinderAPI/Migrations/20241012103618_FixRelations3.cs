using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamFinderAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixRelations3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "User",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "User",
                newName: "ID");
        }
    }
}
