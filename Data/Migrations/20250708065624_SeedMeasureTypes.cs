using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CalorieCalculatorCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedMeasureTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MeasureType",
                columns: new[] { "Id", "Name", "Symbol", "Uri" },
                values: new object[,]
                {
                    { 1, "Gram", "g", "http://www.edamam.com/ontologies/edamam.owl#Measure_gram" },
                    { 2, "Milliliter", "ml", "http://www.edamam.com/ontologies/edamam.owl#Measure_milliliter" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MeasureType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MeasureType",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
