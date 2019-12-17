using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Configuration
{
    class CashDonationConfiguration : IEntityTypeConfiguration<CashDonation>
    {
        public void Configure(EntityTypeBuilder<CashDonation> cashDonation)
        {
            cashDonation
                .HasKey(x => x.Id);

            cashDonation
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            cashDonation
                .Property(x => x.Amount);

            cashDonation
                .Property(x => x.Description)
                .IsRequired(false);
        }
    }
}
