using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzamAfridi.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseTableUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Maintance_Vehicles",
                columns: table => new
                {
                    VehicleMaintance = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_Maintance_Vehicles", x => x.VehicleMaintance);
                    table.ForeignKey(
                        name: "FK_Maintance_Vehicles_RouteDetails_RouteDetailRouteID_RouteDetailBuiltyNo",
                        columns: x => new { x.RouteDetailRouteID, x.RouteDetailBuiltyNo },
                        principalTable: "RouteDetails",
                        principalColumns: new[] { "RouteID", "BuiltyNo" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Maintance_Vehicles_RouteDetailRouteID_RouteDetailBuiltyNo",
                table: "Maintance_Vehicles",
                columns: new[] { "RouteDetailRouteID", "RouteDetailBuiltyNo" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Maintance_Vehicles");
        }
    }
}
