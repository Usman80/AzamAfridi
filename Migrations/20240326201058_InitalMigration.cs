using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AzamAfridi.Migrations
{
    /// <inheritdoc />
    public partial class InitalMigration : Migration
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
                    ExpenseTypeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsExpenseType = table.Column<bool>(type: "bit", nullable: false)
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
                    TotalIncome = table.Column<double>(type: "float", nullable: false),
                    TotalMaintance = table.Column<double>(type: "float", nullable: false),
                    Isbuilty = table.Column<bool>(type: "bit", nullable: false)
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
                    StationDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsStation = table.Column<bool>(type: "bit", nullable: false)
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
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Expense_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Maintance_Vehicles",
                columns: table => new
                {
                    VehicleMaintanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteID = table.Column<int>(type: "int", nullable: false),
                    RouteDetailRouteID = table.Column<int>(type: "int", nullable: false),
                    RouteDetailBuiltyNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Maintance_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Maintance_Price = table.Column<double>(type: "float", nullable: false),
                    Maintance_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintance_Vehicles", x => x.VehicleMaintanceId);
                    table.ForeignKey(
                        name: "FK_Maintance_Vehicles_RouteDetails_RouteDetailRouteID_RouteDetailBuiltyNo",
                        columns: x => new { x.RouteDetailRouteID, x.RouteDetailBuiltyNo },
                        principalTable: "RouteDetails",
                        principalColumns: new[] { "RouteID", "BuiltyNo" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "ExpenseTypeId", "ExpenseTypeCode", "ExpenseTypeDescription", "IsExpenseType" },
                values: new object[,]
                {
                    { 1, "Diesel-Lit", "Diesel Litter", false },
                    { 2, "Fix-Chrg", "Fixed Charges", false },
                    { 3, "TollTax", "Toll Tax", false }
                });

            migrationBuilder.InsertData(
                table: "StationNames",
                columns: new[] { "StationId", "IsStation", "StationCode", "StationDescription" },
                values: new object[,]
                {
                    { 1, false, "LHR", "Lahore" },
                    { 2, false, "KHI", "Karachi" },
                    { 3, false, "GUJ", "Gujrawala" },
                    { 4, false, "MLT", "Multan" },
                    { 5, false, "PESH", "Peshawar" },
                    { 6, false, "MUR", "Murree" },
                    { 7, false, "KHT", "Kohat" },
                    { 8, false, "SHK", "Sheikhapura" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseOnRoutes_ExpenseTypeId",
                table: "ExpenseOnRoutes",
                column: "ExpenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseOnRoutes_RouteDetailRouteID_RouteDetailBuiltyNo",
                table: "ExpenseOnRoutes",
                columns: new[] { "RouteDetailRouteID", "RouteDetailBuiltyNo" });

            migrationBuilder.CreateIndex(
                name: "IX_Maintance_Vehicles_RouteDetailRouteID_RouteDetailBuiltyNo",
                table: "Maintance_Vehicles",
                columns: new[] { "RouteDetailRouteID", "RouteDetailBuiltyNo" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseOnRoutes");

            migrationBuilder.DropTable(
                name: "Maintance_Vehicles");

            migrationBuilder.DropTable(
                name: "StationNames");

            migrationBuilder.DropTable(
                name: "ExpenseTypes");

            migrationBuilder.DropTable(
                name: "RouteDetails");
        }
    }
}
