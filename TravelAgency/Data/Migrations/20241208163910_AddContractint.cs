using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgency.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddContractint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date",
                table: "tour",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "end_date",
                table: "tour",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateIndex(
                name: "IX_tour_purchase_purchase_id",
                table: "tour_purchase",
                column: "purchase_id");

            migrationBuilder.CreateIndex(
                name: "IX_tour_accommodation_id",
                table: "tour",
                column: "accommodation_id");

            migrationBuilder.CreateIndex(
                name: "IX_tour_country_id",
                table: "tour",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_tour_resort_id",
                table: "tour",
                column: "resort_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tour_accommodation_accommodation_id",
                table: "tour",
                column: "accommodation_id",
                principalTable: "accommodation",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tour_country_country_id",
                table: "tour",
                column: "country_id",
                principalTable: "country",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tour_resort_resort_id",
                table: "tour",
                column: "resort_id",
                principalTable: "resort",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tour_purchase_purchase_purchase_id",
                table: "tour_purchase",
                column: "purchase_id",
                principalTable: "purchase",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tour_purchase_tour_tour_id",
                table: "tour_purchase",
                column: "tour_id",
                principalTable: "tour",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tour_accommodation_accommodation_id",
                table: "tour");

            migrationBuilder.DropForeignKey(
                name: "FK_tour_country_country_id",
                table: "tour");

            migrationBuilder.DropForeignKey(
                name: "FK_tour_resort_resort_id",
                table: "tour");

            migrationBuilder.DropForeignKey(
                name: "FK_tour_purchase_purchase_purchase_id",
                table: "tour_purchase");

            migrationBuilder.DropForeignKey(
                name: "FK_tour_purchase_tour_tour_id",
                table: "tour_purchase");

            migrationBuilder.DropIndex(
                name: "IX_tour_purchase_purchase_id",
                table: "tour_purchase");

            migrationBuilder.DropIndex(
                name: "IX_tour_accommodation_id",
                table: "tour");

            migrationBuilder.DropIndex(
                name: "IX_tour_country_id",
                table: "tour");

            migrationBuilder.DropIndex(
                name: "IX_tour_resort_id",
                table: "tour");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date",
                table: "tour",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "end_date",
                table: "tour",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
