using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodingApp.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class SoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "SubCategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "Recipes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "Recipe_Ingredients",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "Primary_Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "FoodItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "Food_Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "Recipe_Ingredients");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "Primary_Categories");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "FoodItems");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "Food_Categories");
        }
    }
}
