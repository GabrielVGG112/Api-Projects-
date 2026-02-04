using FoodingApp.Library;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodingApp.EfCore.Configuration
{
    public class CategoryModelConfiguration : IEntityTypeConfiguration<FoodCategory>
    {
        public void Configure(EntityTypeBuilder<FoodCategory> options)
        {
            options.ToTable("Food_Categories");


            options.HasKey(fc => fc.Id);
            options.HasAlternateKey(fc => new { fc.PrimaryGroupId, fc.SubCategoryId });

            options
                .HasMany(x => x.FoodItems)
                .WithOne(x => x.Category);

            options.Property<bool>("is_deleted").HasDefaultValue(false);


            options
                .HasQueryFilter(e => EF.Property<bool>(e, "is_deleted") == false);

        }
    }
}
