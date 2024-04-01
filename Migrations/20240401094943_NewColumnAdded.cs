using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzamAfridi.Migrations
{
    /// <inheritdoc />
    public partial class NewColumnAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DieselLitre",
                table: "ExpenseOnRoutes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DieselLitre",
                table: "ExpenseOnRoutes");
        }
    }
}
