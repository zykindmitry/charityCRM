using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Configuration
{
    internal class WardAddressConfiguration : IEntityTypeConfiguration<Ward>
    {
        public void Configure(EntityTypeBuilder<Ward> ward)
        {
            ward.OwnsOne(o => o.Address, address =>
            {
                address.ToTable(nameof(Ward));
                
                address
                    .Property(p => p.PostCode)
                        .HasColumnName(nameof(Address.PostCode))
                        .HasMaxLength(Address.PostCodeMaxLength)
                        .IsRequired(Address.PostCodeIsRequired);

                address
                    .Property(p => p.Country)
                        .HasColumnName(nameof(Address.Country))
                        .HasMaxLength(Address.CountryMaxLength)
                        .IsRequired(Address.CountryIsRequired);

                address
                    .Property(p => p.Region)
                        .HasColumnName(nameof(Address.Region))
                        .HasMaxLength(Address.RegionMaxLength)
                        .IsRequired(Address.RegionIsRequired);

                address
                    .Property(p => p.City)
                        .HasColumnName(nameof(Address.City))
                        .HasMaxLength(Address.CityMaxLength)
                        .IsRequired(Address.CityIsRequired);

                address
                    .Property(p => p.Area)
                        .HasColumnName(nameof(Address.Area))
                        .HasMaxLength(Address.AreaMaxLength)
                        .IsRequired(Address.AreaIsRequired);

                address
                    .Property(p => p.Street)
                        .HasColumnName(nameof(Address.Street))
                        .HasMaxLength(Address.StreetMaxLength)
                        .IsRequired(Address.StreetIsRequired);

                address
                    .Property(p => p.House)
                        .HasColumnName(nameof(Address.House))
                        .HasMaxLength(Address.HouseMaxLength)
                        .IsRequired(Address.HouseIsRequired);

                address
                    .Property(p => p.Flat)
                        .HasColumnName(nameof(Address.Flat))
                        .HasMaxLength(Address.FlatMaxLength)
                        .IsRequired(Address.FlatIsRequired);
            });
        }
    }
}
