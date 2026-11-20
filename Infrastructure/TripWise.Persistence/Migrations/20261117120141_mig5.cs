using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripWise.Persistence.Migrations;

/// <inheritdoc />
public partial class _20261117120141_mig5 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_TripDestinations_TripId",
            table: "TripDestinations");

        migrationBuilder.CreateIndex(
            name: "IX_TripDestinations_TripId_DestinationId",
            table: "TripDestinations",
            columns: new[] { "TripId", "DestinationId" },
            unique: true,
            filter: "[IsDeleted] = 0");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_TripDestinations_TripId_DestinationId",
            table: "TripDestinations");

        migrationBuilder.CreateIndex(
            name: "IX_TripDestinations_TripId",
            table: "TripDestinations",
            column: "TripId");
    }
}
