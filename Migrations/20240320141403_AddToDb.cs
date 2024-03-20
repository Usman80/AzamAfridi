using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AzamAfridi.Migrations
{
    /// <inheritdoc />
    public partial class AddToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpenseTypes",
                columns: table => new
                {
                    ExpenseTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpenseTypeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseTypes", x => x.ExpenseTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RouteDetails",
                columns: table => new
                {
                    RouteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuiltyNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DriveName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TruckNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Start_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    FromStation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToStation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromFare = table.Column<double>(type: "float", nullable: false),
                    Return_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Return_Weight = table.Column<int>(type: "int", nullable: false),
                    Return_FromStation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Return_ToStation = table.Column<string>(type: "nvarchar(max)", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "ExpenseOnRoutes",
                columns: table => new
                {
                    ExpenseOnRouteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteID = table.Column<int>(type: "int", nullable: false),
                    RouteDetailRouteID = table.Column<int>(type: "int", nullable: false),
                    RouteDetailBuiltyNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExpenseTypeId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseOnRoutes", x => x.ExpenseOnRouteID);
                    table.ForeignKey(
                        name: "FK_ExpenseOnRoutes_ExpenseTypes_ExpenseTypeId",
                        column: x => x.ExpenseTypeId,
                        principalTable: "ExpenseTypes",
                        principalColumn: "ExpenseTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpenseOnRoutes_RouteDetails_RouteDetailRouteID_RouteDetailBuiltyNo",
                        columns: x => new { x.RouteDetailRouteID, x.RouteDetailBuiltyNo },
                        principalTable: "RouteDetails",
                        principalColumns: new[] { "RouteID", "BuiltyNo" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "ExpenseTypeId", "ExpenseTypeCode", "ExpenseTypeDescription" },
                values: new object[,]
                {
                    { 1, "Diesel-Lit", "Diesel Litter" },
                    { 2, "Fix-Chrg", "Fixed Charges" },
                    { 3, "TollTax", "Toll Tax" }
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

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseOnRoutes_ExpenseTypeId",
                table: "ExpenseOnRoutes",
                column: "ExpenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseOnRoutes_RouteDetailRouteID_RouteDetailBuiltyNo",
                table: "ExpenseOnRoutes",
                columns: new[] { "RouteDetailRouteID", "RouteDetailBuiltyNo" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseOnRoutes");

            migrationBuilder.DropTable(
                name: "StationNames");

            migrationBuilder.DropTable(
                name: "ExpenseTypes");

            migrationBuilder.DropTable(
                name: "RouteDetails");
        }
    }
}
