using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TravelAgency.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTourWithPurchases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "purchase",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    total_price = table.Column<decimal>(type: "numeric", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tour",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    resort_id = table.Column<int>(type: "integer", nullable: false),
                    accommodation_id = table.Column<int>(type: "integer", nullable: false),
                    photo_path = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tour", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tour_purchase",
                columns: table => new
                {
                    tour_id = table.Column<int>(type: "integer", nullable: false),
                    purchase_id = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tour_purchase", x => new { x.tour_id, x.purchase_id });
                });

            migrationBuilder.CreateIndex(
                name: "IX_tour_name",
                table: "tour",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "purchase");

            migrationBuilder.DropTable(
                name: "tour");

            migrationBuilder.DropTable(
                name: "tour_purchase");
        }
    }
}
