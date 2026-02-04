using FoodingApp.Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodingApp.EfCore.Configuration
{
    public class ItemModelConfiguration : IEntityTypeConfiguration<FoodItemModel>
    {


        public void Configure(EntityTypeBuilder<FoodItemModel> options)
        {
            options.HasKey(x => x.Id);

            options
                .HasOne(x => x.Category)
                .WithMany(x => x.FoodItems)
                .HasForeignKey(x => x.CategoryId);

            options
                .HasOne(x => x.Category);
            options
            .OwnsOne(x => x.Nutrients, b =>
            {
                b.ToJson();
                b.OwnsOne(x => x.Macros);
                b.OwnsOne(x => x.Minerals);
                b.OwnsOne(x => x.Vitamins);
            });
            options.Property<bool>("is_deleted").HasDefaultValue(false);
            options
               .HasQueryFilter(e => EF.Property<bool>(e, "is_deleted") == false);
        }

    }
}
