using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgency.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnusedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "comment");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "comment",
                newName: "message");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "message",
                table: "comment",
                newName: "title");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "comment",
                type: "text",
                nullable: true);
        }
    }
}
