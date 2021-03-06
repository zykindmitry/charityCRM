﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Configuration
{
    internal class CommodityConfiguration : IEntityTypeConfiguration<Commodity>
    {
        public void Configure(EntityTypeBuilder<Commodity> commodity)
        {
            commodity.ToTable(nameof(Commodity));

            commodity.HasKey(x => x.Id);

            commodity.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            commodity.Property(x => x.Description)
                .HasMaxLength(Commodity.DescriptionMaxLength)
                .IsRequired(Commodity.DescriptionIsRequired);

            commodity.Property(x => x.Quantity).IsRequired();

            commodity.Property(x => x.Cost).IsRequired(false);

            /*//Для разрешения каскадного удаления связанных commodities при удалении CommodityDonation
            commodity.Property(typeof(long), "CommodityDonationId");*/
        }
    }
}
