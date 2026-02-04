using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodingApp.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class CreationAndUpdateDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "Recipes",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<DateTime>(
                name: "modified_at",
                table: "Recipes",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "Recipe_Ingredients",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<DateTime>(
                name: "modified_at",
                table: "Recipe_Ingredients",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "modified_at",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Recipe_Ingredients");

            migrationBuilder.DropColumn(
                name: "modified_at",
                table: "Recipe_Ingredients");
        }
    }
}
