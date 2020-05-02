using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Configuration
{
    internal class WardConfiguration : IEntityTypeConfiguration<Ward>
    {
        public void Configure(EntityTypeBuilder<Ward> ward)
        {
            ward
                .HasKey(x => x.Id);

            ward
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            ward
                .Property(x => x.CreatedAt)
                .IsRequired(true);
            
            ward
                .Property(x => x.BirthDate)
                .IsRequired(true);

            ward
                .Property(x => x.Phone);

            ward
                .HasMany(x => x.WardCategories)
                .WithOne()
                .HasForeignKey("WardId");
            
            var wardCategoryNavigation = ward.Metadata.FindNavigation(nameof(Ward.WardCategories));
            wardCategoryNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
            wardCategoryNavigation.SetField(Ward.ThisWardCategoriesFieldName);
        }
    }
}
