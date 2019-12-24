using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Configuration
{
    internal class DonationConfiguration : IEntityTypeConfiguration<Donation>
    {
        public void Configure(EntityTypeBuilder<Donation> donation)
        {            
            donation.ToTable("Donations")
                .HasDiscriminator<string>("DonationType")
                .HasValue<CashDonation>(nameof(CashDonation))
                .HasValue<CommodityDonation>(nameof(CommodityDonation));

            donation.HasKey(d => d.Id);

            donation.Property(d => d.Id)
                .ValueGeneratedOnAdd();

            donation.Property(d => d.Description)
                .IsRequired(true);
        }
    }
}
