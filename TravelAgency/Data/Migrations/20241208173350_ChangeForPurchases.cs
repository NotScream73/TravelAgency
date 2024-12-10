using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TravelAgency.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeForPurchases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tour_purchase",
                table: "tour_purchase");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "tour_purchase",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "tour_purchase",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "purchase",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tour_purchase",
                table: "tour_purchase",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_tour_purchase_tour_id",
                table: "tour_purchase",
                column: "tour_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tour_purchase",
                table: "tour_purchase");

            migrationBuilder.DropIndex(
                name: "IX_tour_purchase_tour_id",
                table: "tour_purchase");

            migrationBuilder.DropColumn(
                name: "id",
                table: "tour_purchase");

            migrationBuilder.AlterColumn<double>(
                name: "price",
                table: "tour_purchase",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "purchase",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tour_purchase",
                table: "tour_purchase",
                columns: new[] { "tour_id", "purchase_id" });
        }
    }
}
