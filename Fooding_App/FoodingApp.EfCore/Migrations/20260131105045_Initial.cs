using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodingApp.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Primary_Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Category_Id", x => x.Id);
                    table.UniqueConstraint("Category_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Recipe_Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QuantityInUnitValue = table.Column<double>(type: "REAL", precision: 10, scale: 2, nullable: false),
                    Grams = table.Column<double>(type: "REAL", precision: 10, scale: 2, nullable: false),
                    UnitType = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "Gram"),
                    NutriScore = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_Recipe_Ingredients_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Instructions = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: false),
                    PreparationTime = table.Column<int>(type: "INTEGER", nullable: false),
                    PreparationMethod = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    Difficulty = table.Column<int>(type: "INTEGER", nullable: false),
                    Kcal = table.Column<double>(type: "REAL", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("recipe_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SubCategory_Id", x => x.Id);
                    table.UniqueConstraint("SubCategory_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredientRecipeModel",
                columns: table => new
                {
                    IngredientsId = table.Column<int>(type: "INTEGER", nullable: false),
                    RecipeesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredientRecipeModel", x => new { x.IngredientsId, x.RecipeesId });
                    table.ForeignKey(
                        name: "FK_RecipeIngredientRecipeModel_Recipe_Ingredients_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Recipe_Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredientRecipeModel_Recipes_RecipeesId",
                        column: x => x.RecipeesId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Food_Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PrimaryGroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubCategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food_Categories", x => x.Id);
                    table.UniqueConstraint("AK_Food_Categories_PrimaryGroupId_SubCategoryId", x => new { x.PrimaryGroupId, x.SubCategoryId });
                    table.ForeignKey(
                        name: "FK_Food_Categories_Primary_Categories_PrimaryGroupId",
                        column: x => x.PrimaryGroupId,
                        principalTable: "Primary_Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Food_Categories_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Nutrients = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodItems_Food_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Food_Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Food_Categories_SubCategoryId",
                table: "Food_Categories",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_CategoryId",
                table: "FoodItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredientRecipeModel_RecipeesId",
                table: "RecipeIngredientRecipeModel",
                column: "RecipeesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodItems");

            migrationBuilder.DropTable(
                name: "RecipeIngredientRecipeModel");

            migrationBuilder.DropTable(
                name: "Food_Categories");

            migrationBuilder.DropTable(
                name: "Recipe_Ingredients");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Primary_Categories");

            migrationBuilder.DropTable(
                name: "SubCategories");
        }
    }
}
