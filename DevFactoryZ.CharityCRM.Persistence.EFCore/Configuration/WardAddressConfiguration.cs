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
                address.Ignore(p => p.FullAddress);

            });
        }
    }
}
