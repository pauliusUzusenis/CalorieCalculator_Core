using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalorieCalculatorCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSymbolToMeasureTypeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "MeasureType",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "MeasureType");
        }
    }
}
