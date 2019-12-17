using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Configuration
{
    class CommodityConfiguration : IEntityTypeConfiguration<Commodity>
    {
        public void Configure(EntityTypeBuilder<Commodity> commodity)
        {
            commodity
                .HasKey(x => x.Id);

            commodity
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            commodity
                .Property(x => x.Description)
                .HasMaxLength(Commodity.DescriptionMaxLength)
                .IsRequired(Commodity.DescriptionIsRequired);

            commodity
                .Property(x => x.Quantity);

            commodity
                .Property(x => x.Cost)
                .IsRequired(false);

            // Настройка внешнего ключа
            commodity
                .HasOne(x => x.CommodityDonation)
                    .WithMany(p => p.Commodities)
                .HasForeignKey(x => x.CommodityDonationId);
        }
    }
}
