using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Configuration
{
    internal class CommodityDonationConfiguration : IEntityTypeConfiguration<CommodityDonation>
    {
        public void Configure(EntityTypeBuilder<CommodityDonation> commodityDonation)
        {
            commodityDonation.HasMany(x => x.Commodities).WithOne().HasForeignKey("DonationId").IsRequired();
        }
    }
}
