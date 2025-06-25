using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalorieCalculatorCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToMenuModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Menu",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Menu");
        }
    }
}
