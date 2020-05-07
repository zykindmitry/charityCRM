using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Configuration
{
    internal class WardCategorySubCategoryConfiguration : IEntityTypeConfiguration<WardCategory.WardCategorySubCategory>
    {
        public void Configure(EntityTypeBuilder<WardCategory.WardCategorySubCategory> wardCategorySubCategory)
        {
            wardCategorySubCategory.ToTable("WardCategoriesSubCategories").HasKey("WardCategoryId", "SubCategoryId");
            wardCategorySubCategory.HasOne(x => x.WardCategory).WithMany().HasForeignKey("SubCategoryId").OnDelete(DeleteBehavior.Restrict);
        }
    }
}
