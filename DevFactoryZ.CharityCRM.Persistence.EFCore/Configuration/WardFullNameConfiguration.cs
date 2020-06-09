using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Configuration
{
    internal class WardFullNameConfiguration : IEntityTypeConfiguration<Ward>
    {
        public void Configure(EntityTypeBuilder<Ward> ward)
        {
            ward.OwnsOne(w => w.FullName, fullName =>
            {
                fullName.ToTable(nameof(Ward));
                
                fullName
                    .Property(p => p.FirstName)
                        .HasColumnName(nameof(FullName.FirstName))
                        .HasMaxLength(FullName.FirstNameMaxLength)
                        .IsRequired(FullName.FirstNameIsRequired);
                
                fullName
                    .Property(p => p.MiddleName)
                        .HasColumnName(nameof(FullName.MiddleName))
                        .HasMaxLength(FullName.MiddleNameMaxLength)
                        .IsRequired(FullName.MiddleNameIsRequired);
                
                fullName
                    .Property(p => p.SurName)
                        .HasColumnName(nameof(FullName.SurName))
                        .HasMaxLength(FullName.SurNameMaxLength)
                        .IsRequired(FullName.SurNameIsRequired);
            });
        }
    }
}
