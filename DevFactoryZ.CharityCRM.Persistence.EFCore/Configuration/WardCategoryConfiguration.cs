using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Configuration
{
    internal class WardCategoryConfiguration : IEntityTypeConfiguration<WardCategory>
    {
        public void Configure(EntityTypeBuilder<WardCategory> wardCategory)
        {
            wardCategory.HasKey(x => x.Id);
            wardCategory.Property(x => x.Id).ValueGeneratedOnAdd();

            wardCategory
                .Property(x => x.Name)
                    .HasMaxLength(WardCategory.NameMaxLength)
                    .IsRequired(WardCategory.NameIsRequired);

            wardCategory.Ignore(x => x.SubCategories);
        }
    }
}
