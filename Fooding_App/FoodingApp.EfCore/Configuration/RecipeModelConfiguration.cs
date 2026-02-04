using FoodingApp.Library.Enums;
using FoodingApp.Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodingApp.EfCore.Configuration
{
    internal class RecipeModelConfiguration : IEntityTypeConfiguration<RecipeModel>
    {
        public void Configure(EntityTypeBuilder<RecipeModel> options)
        {
            options.ToTable("Recipes");


            options
                .HasKey(r => r.Id)
                .HasName("recipe_Id");

            options
                .HasMany(x => x.Ingredients)
                .WithMany(x => x.Recipees);

            options
                .Property(r => r.Instructions)
                .HasMaxLength(4000);

            options
                .Property(r => r.Name)
                .HasMaxLength(200)
                .IsRequired();
            options
                .Property(r => r.PreparationMethod)
                .HasDefaultValue(PreparationMethodEnum.None);
            options
                .Property(r => r.Kcal)
                .HasField("_kcal");
            options
                .Property(r => r.Difficulty)
                .HasField("_dificulty");
            options
                .Property<bool>("is_deleted").HasDefaultValue(false);
            options
               .HasQueryFilter(e =>
               EF.Property<bool>(e, "is_deleted") == false
               );



            options
                .Property<DateTime>("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();
            options.Property<DateTime>("modified_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();

        }
    }
}
