using FoodingApp.EfCore.Configuration.Converters;
using FoodingApp.Library.Extensions;
using FoodingApp.Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodingApp.EfCore.Configuration
{
    public class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredient>
    {
        public void Configure(EntityTypeBuilder<RecipeIngredient> options)
        {
            options.ToTable("Recipe_Ingredients");
            options.HasKey(x => x.Id).HasName("Pk_Recipe_Ingredients_Id");

            options.HasMany(x => x.Recipees)
                   .WithMany(x => x.Ingredients); //  # A 

            options
                .Ignore(x => x.FoodItem); //    # B


            options
                .Property(x => x.Grams)
                .HasField("_grams");

            options
                .Property(x => x.NutriScore)
                .HasField("_nutriscore");

            //  -- Converters --
            options
                .Property(x => x.UnitType)
                .HasConversion(new UnitEnumConverter())
                .HasDefaultValue(UnitEnum.Gram);

            options
                .Property(x => x.NutriScore)
                .HasConversion(new NutriScoreEnumConverter());
            options.Property<bool>("is_deleted")
                .HasDefaultValue(false);

            options
               .HasQueryFilter(e => EF.Property<bool>(e, "is_deleted") == false);

            options
                .Property<DateTime>("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            options
                .Property<DateTime>("modified_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
        }
    }
}
/*
    #A -  Default  Many To many Relationship between RecipeeItem And Recipee model 
    #B- Based on FoodItem the RecipeeItem calculates diferent fields
*/