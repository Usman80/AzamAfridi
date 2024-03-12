using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AzamAfridi.Migrations
{
    /// <inheritdoc />
    public partial class addTablesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RouteDetails",
                columns: table => new
                {
                    RouteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuiltyNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DriveName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TruckNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    FromStation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToStation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromFare = table.Column<double>(type: "float", nullable: false),
                    ToFare = table.Column<double>(type: "float", nullable: false),
                    TotalFare = table.Column<double>(type: "float", nullable: false),
                    TotalExpense = table.Column<double>(type: "float", nullable: false),
                    TotalIncome = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteDetails", x => new { x.RouteID, x.BuiltyNo });
                });

            migrationBuilder.CreateTable(
                name: "StationNames",
                columns: table => new
                {
                    StationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StationDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationNames", x => x.StationId);
                });

            migrationBuilder.InsertData(
                table: "StationNames",
                columns: new[] { "StationId", "StationCode", "StationDescription" },
                values: new object[,]
                {
                    { 1, "LHR", "Lahore" },
                    { 2, "KHI", "Karachi" },
                    { 3, "GUJ", "Gujrawala" },
                    { 4, "MLT", "Multan" },
                    { 5, "PESH", "Peshawar" },
                    { 6, "MUR", "Murree" },
                    { 7, "KHT", "Kohat" },
                    { 8, "SHK", "Sheikhapura" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RouteDetails");

            migrationBuilder.DropTable(
                name: "StationNames");
        }
    }
}
