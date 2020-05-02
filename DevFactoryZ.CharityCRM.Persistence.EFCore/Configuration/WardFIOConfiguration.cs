using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Configuration
{
    internal class WardFIOConfiguration : IEntityTypeConfiguration<Ward>
    {
        public void Configure(EntityTypeBuilder<Ward> ward)
        {
            ward.OwnsOne(w => w.FIO, fio =>
            {
                fio.ToTable(nameof(Ward));
                
                fio.Property(p => p.FirstName);
                
                fio.Property(p => p.MidName);
                
                fio.Property(p => p.LastName);
                
                fio.Ignore(p => p.FullName);
            });
        }
    }
}
