using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AzamAfridi.Migrations
{
    /// <inheritdoc />
    public partial class AddExpenseType : Migration
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

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "ExpenseTypeId", "ExpenseTypeCode", "ExpenseTypeDescription" },
                values: new object[,]
                {
                    { 1, "Diesel-Lit", "Diesel Litter" },
                    { 2, "Fix-Chrg", "Fixed Charges" },
                    { 3, "TollTax", "Toll Tax" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseTypes");
        }
    }
}
