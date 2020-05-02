using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Configuration
{
    internal class ThisWardCategoryConfiguration : IEntityTypeConfiguration<Ward.ThisWardCategory>
    {
        public void Configure(EntityTypeBuilder<Ward.ThisWardCategory> thisWardCategory)
        {
            thisWardCategory.ToTable("WardCategories").HasKey("WardId", "WardCategoryId");
            thisWardCategory.HasOne(x => x.WardCategory).WithMany().HasForeignKey("WardCategoryId");
        }
    }
}
