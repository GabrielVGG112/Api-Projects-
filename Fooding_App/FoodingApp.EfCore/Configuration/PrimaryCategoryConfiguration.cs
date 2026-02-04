using FoodingApp.Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodingApp.EfCore.Configuration
{
    public class PrimaryCategoryConfiguration : IEntityTypeConfiguration<PrimaryCategory>
    {
        public void Configure(EntityTypeBuilder<PrimaryCategory> options)
        {
            options
               .ToTable("Primary_Categories");

            options
                .HasKey(x => x.Id).HasName("Category_Id");

            options.HasAlternateKey(x => x.Name).HasName("Category_Name");


            options
                .Property(x => x.Name)
                .IsRequired();
            options.Property<bool>("is_deleted")
                .HasDefaultValue(false);
            options
               .HasQueryFilter(e => EF.Property<bool>(e, "is_deleted") == false);
        }
    }
}
