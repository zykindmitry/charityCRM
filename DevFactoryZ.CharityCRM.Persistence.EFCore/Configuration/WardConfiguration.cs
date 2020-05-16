using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Configuration
{
    internal class WardConfiguration : IEntityTypeConfiguration<Ward>
    {
        public void Configure(EntityTypeBuilder<Ward> ward)
        {
            ward.HasKey(x => x.Id);

            ward.Property(x => x.Id).ValueGeneratedOnAdd();

            ward.Property(x => x.CreatedAt).IsRequired(true);
            
            ward.Property(x => x.BirthDate).IsRequired(true);

            ward
                .Property(x => x.Phone)
                    .HasMaxLength(Ward.PhoneMaxLength)
                    .IsRequired(Ward.PhoneIsRequired);

            ward.HasMany(x => x.WardCategories).WithOne().HasForeignKey("WardId");            
        }
    }
}
