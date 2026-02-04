using FoodingApp.Library;
using FoodingApp.Library.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodingApp.EfCore;

public class FoodingAppDb : DbContext
{

    public FoodingAppDb()
    {

    }

    public FoodingAppDb(DbContextOptions options) : base(options)
    {

    }
    public DbSet<FoodItemModel> FoodItems { get; set; }
    public DbSet<PrimaryCategory> PrimaryCategories { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<FoodCategory> Categories { get; set; }
    public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    public DbSet<RecipeModel> Recipees { get; set; }



    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>().HaveMaxLength(100);
        configurationBuilder.Properties<double>().HavePrecision(10, 2);



    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration(new ItemModelConfiguration());
        //modelBuilder.ApplyConfiguration(new RecipeIngredientConfiguration());
        //modelBuilder.ApplyConfiguration(new RecipeModelConfiguration());
        //modelBuilder.ApplyConfiguration(new CategoryModelConfiguration());
        //modelBuilder.ApplyConfiguration(new SecondaryCategoryConfiguration());
        //modelBuilder.ApplyConfiguration(new PrimaryCategoryConfiguration());

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FoodingAppDb).Assembly);

    }

}
