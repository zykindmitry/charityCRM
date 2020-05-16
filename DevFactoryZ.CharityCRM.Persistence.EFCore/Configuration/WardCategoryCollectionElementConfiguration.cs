using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Configuration
{
    internal class WardCategoryCollectionElementConfiguration : IEntityTypeConfiguration<Ward.WardCategoryCollectionElement>
    {
        public void Configure(EntityTypeBuilder<Ward.WardCategoryCollectionElement> wardCategoryCollectionElement)
        {
            wardCategoryCollectionElement.ToTable("WardCategories").HasKey("WardId", "WardCategoryId");
            wardCategoryCollectionElement
                .HasOne(x => x.WardCategory)
                .WithMany()
                .HasForeignKey("WardCategoryId")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
