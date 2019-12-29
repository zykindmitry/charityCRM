using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Configuration
{
    internal class FundRegistrationConfiguration : IEntityTypeConfiguration<FundRegistration>
    {
        public void Configure(EntityTypeBuilder<FundRegistration> fundRegistration)
        {
            fundRegistration.HasKey(x => x.Id);

            fundRegistration
                .Property(x => x.Name)
                    .HasMaxLength(FundRegistration.NameMaxLength)
                    .IsRequired(FundRegistration.NameIsRequired);

            fundRegistration.Property(x => x.Description).IsRequired(false);
            fundRegistration.Property(x => x.MaxLifeTime).IsRequired();
            fundRegistration.Property(x => x.CreatedAt).IsRequired();
            fundRegistration.Property(x => x.SucceededAt).IsRequired(false);


        }
    }
}
