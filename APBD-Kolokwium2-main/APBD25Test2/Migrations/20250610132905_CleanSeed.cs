using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APBD25Test2.Migrations
{
    /// <inheritdoc />
    public partial class CleanSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Race",
                keyColumn: "RaceId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Race",
                keyColumn: "RaceId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Race_Participation",
                keyColumns: new[] { "RacerId", "TrackRaceId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "Race_Participation",
                keyColumns: new[] { "RacerId", "TrackRaceId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "Racer",
                keyColumn: "RacerId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Racer",
                keyColumn: "RacerId",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Race",
                keyColumn: "RaceId",
                keyValue: 1,
                columns: new[] { "Date", "Location", "Name" },
                values: new object[] { new DateTime(2025, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Silverstone, UK", "British Grand Prix" });

            migrationBuilder.UpdateData(
                table: "Race",
                keyColumn: "RaceId",
                keyValue: 2,
                columns: new[] { "Date", "Location", "Name" },
                values: new object[] { new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Monte Carlo, Monaco", "Monaco Grand Prix" });

            migrationBuilder.UpdateData(
                table: "Race_Participation",
                keyColumns: new[] { "RacerId", "TrackRaceId" },
                keyValues: new object[] { 1, 1 },
                column: "FinishTimeInSeconds",
                value: 5460);

            migrationBuilder.UpdateData(
                table: "Race_Participation",
                keyColumns: new[] { "RacerId", "TrackRaceId" },
                keyValues: new object[] { 2, 2 },
                columns: new[] { "FinishTimeInSeconds", "Position" },
                values: new object[] { 6200, 1 });

            migrationBuilder.InsertData(
                table: "Race_Participation",
                columns: new[] { "RacerId", "TrackRaceId", "FinishTimeInSeconds", "Position" },
                values: new object[] { 1, 2, 6300, 2 });

            migrationBuilder.UpdateData(
                table: "Racer",
                keyColumn: "RacerId",
                keyValue: 1,
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { "Lewis", "Hamilton" });

            migrationBuilder.UpdateData(
                table: "Racer",
                keyColumn: "RacerId",
                keyValue: 2,
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { "Max", "Verstappen" });

            migrationBuilder.UpdateData(
                table: "Track",
                keyColumn: "TrackId",
                keyValue: 1,
                columns: new[] { "LengthInKm", "Name" },
                values: new object[] { 5.89m, "Silverstone Circuit" });

            migrationBuilder.UpdateData(
                table: "Track",
                keyColumn: "TrackId",
                keyValue: 2,
                columns: new[] { "LengthInKm", "Name" },
                values: new object[] { 3.34m, "Monaco Circuit" });

            migrationBuilder.UpdateData(
                table: "Track_Race",
                keyColumn: "TrackRaceId",
                keyValue: 1,
                column: "BestTimeInSeconds",
                value: 5460);

            migrationBuilder.UpdateData(
                table: "Track_Race",
                keyColumn: "TrackRaceId",
                keyValue: 2,
                column: "BestTimeInSeconds",
                value: 6300);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Race_Participation",
                keyColumns: new[] { "RacerId", "TrackRaceId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.UpdateData(
                table: "Race",
                keyColumn: "RaceId",
                keyValue: 1,
                columns: new[] { "Date", "Location", "Name" },
                values: new object[] { new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "London", "British Race" });

            migrationBuilder.UpdateData(
                table: "Race",
                keyColumn: "RaceId",
                keyValue: 2,
                columns: new[] { "Date", "Location", "Name" },
                values: new object[] { new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Paris", "French Race" });

            migrationBuilder.InsertData(
                table: "Race",
                columns: new[] { "RaceId", "Date", "Location", "Name" },
                values: new object[,]
                {
                    { 3, new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dubai", "UAE Race" },
                    { 4, new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Monaco", "Monaco Race" }
                });

            migrationBuilder.UpdateData(
                table: "Race_Participation",
                keyColumns: new[] { "RacerId", "TrackRaceId" },
                keyValues: new object[] { 1, 1 },
                column: "FinishTimeInSeconds",
                value: 200);

            migrationBuilder.UpdateData(
                table: "Race_Participation",
                keyColumns: new[] { "RacerId", "TrackRaceId" },
                keyValues: new object[] { 2, 2 },
                columns: new[] { "FinishTimeInSeconds", "Position" },
                values: new object[] { 1200, 2 });

            migrationBuilder.UpdateData(
                table: "Racer",
                keyColumn: "RacerId",
                keyValue: 1,
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { "John", "Doe" });

            migrationBuilder.UpdateData(
                table: "Racer",
                keyColumn: "RacerId",
                keyValue: 2,
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { "Bob", "Doe" });

            migrationBuilder.InsertData(
                table: "Racer",
                columns: new[] { "RacerId", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 3, "Robert", "Doe" },
                    { 4, "Nadia", "Doe" }
                });

            migrationBuilder.UpdateData(
                table: "Track",
                keyColumn: "TrackId",
                keyValue: 1,
                columns: new[] { "LengthInKm", "Name" },
                values: new object[] { 2.49m, "Fast Track" });

            migrationBuilder.UpdateData(
                table: "Track",
                keyColumn: "TrackId",
                keyValue: 2,
                columns: new[] { "LengthInKm", "Name" },
                values: new object[] { 4.64m, "Slow Track" });

            migrationBuilder.UpdateData(
                table: "Track_Race",
                keyColumn: "TrackRaceId",
                keyValue: 1,
                column: "BestTimeInSeconds",
                value: 200);

            migrationBuilder.UpdateData(
                table: "Track_Race",
                keyColumn: "TrackRaceId",
                keyValue: 2,
                column: "BestTimeInSeconds",
                value: 800);

            migrationBuilder.InsertData(
                table: "Race_Participation",
                columns: new[] { "RacerId", "TrackRaceId", "FinishTimeInSeconds", "Position" },
                values: new object[,]
                {
                    { 3, 1, 500, 2 },
                    { 4, 2, 800, 1 }
                });
        }
    }
}
