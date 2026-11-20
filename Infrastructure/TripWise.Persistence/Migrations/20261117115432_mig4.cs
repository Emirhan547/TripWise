using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripWise.Persistence.Migrations;

/// <inheritdoc />
public partial class _20261117115432_mig4 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateIndex(
            name: "IX_TripDestinations_DestinationId",
            table: "TripDestinations",
            column: "DestinationId");

        migrationBuilder.CreateIndex(
            name: "IX_TripDestinations_TripId",
            table: "TripDestinations",
            column: "TripId");

        migrationBuilder.AddForeignKey(
            name: "FK_TripDestinations_Destinations_DestinationId",
            table: "TripDestinations",
            column: "DestinationId",
            principalTable: "Destinations",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);

        migrationBuilder.AddForeignKey(
            name: "FK_TripDestinations_Trips_TripId",
            table: "TripDestinations",
            column: "TripId",
            principalTable: "Trips",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_TripDestinations_Destinations_DestinationId",
            table: "TripDestinations");

        migrationBuilder.DropForeignKey(
            name: "FK_TripDestinations_Trips_TripId",
            table: "TripDestinations");

        migrationBuilder.DropIndex(
            name: "IX_TripDestinations_DestinationId",
            table: "TripDestinations");

        migrationBuilder.DropIndex(
            name: "IX_TripDestinations_TripId",
            table: "TripDestinations");
    }
}
