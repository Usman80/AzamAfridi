using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzamAfridi.Migrations
{
    /// <inheritdoc />
    public partial class ExpenseOnRoute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
