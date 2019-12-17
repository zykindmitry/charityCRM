using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Configuration
{
    class CommodityDonationConfiguration : IEntityTypeConfiguration<CommodityDonation>
    {
        public void Configure(EntityTypeBuilder<CommodityDonation> commodityDonation)
        {
            commodityDonation
                .HasKey(x => x.Id);

            commodityDonation
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            commodityDonation
                .Property(x => x.Description)
                .IsRequired(false);

            commodityDonation
                .HasMany(x => x.Commodities)
                .WithOne();

            var commodityNavigation = commodityDonation.Metadata.FindNavigation(nameof(CommodityDonation.Commodities));
            commodityNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
            commodityNavigation.SetField(CommodityDonation.CommoditiesFieldName);
        }
    }
}
