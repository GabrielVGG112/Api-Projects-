using FoodingApp.Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodingApp.EfCore.Configuration;

public class SecondaryCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
{
    public void Configure(EntityTypeBuilder<SubCategory> options)
    {
        options
            .ToTable("SubCategories");
        options
            .HasKey("Id")
            .HasName("SubCategory_Id");

        options
            .HasAlternateKey(x => x.Name)
            .HasName("SubCategory_Name");
        options
            .Property<bool>("is_deleted")
            .HasDefaultValue(false);

        options.HasQueryFilter(e =>
        EF.Property<bool>(e, "is_deleted") == false
        );


    }
}
