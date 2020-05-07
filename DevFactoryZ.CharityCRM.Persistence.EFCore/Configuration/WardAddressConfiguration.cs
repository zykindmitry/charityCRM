using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Configuration
{
    internal class WardAddressConfiguration : IEntityTypeConfiguration<Ward>
    {
        public void Configure(EntityTypeBuilder<Ward> ward)
        {
            ward.OwnsOne(o => o.Address, address =>
            {
                address.ToTable(nameof(Address));
                
                address.Property(p => p.PostCode);

                address.Property(p => p.Country);

                address.Property(p => p.Region);

                address.Property(p => p.City);

                address.Property(p => p.Area);

                address.Property(p => p.Street);

                address.Property(p => p.House);

                address.Property(p => p.Flat);
            });
        }
    }
}
