using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Configuration
{
    internal class FundRegistrationConfiguration : IEntityTypeConfiguration<FundRegistration>
    {
        public void Configure(EntityTypeBuilder<FundRegistration> fundRegistration)
        {
            fundRegistration.HasKey(x => x.Id);
            fundRegistration.Property(x => x.Id).IsRequired();
            fundRegistration.Property(x => x.Name).IsRequired().HasMaxLength(100);
            fundRegistration.Property(x => x.Description).IsRequired();
            fundRegistration.Property(x => x.MaxLifeTime).IsRequired();
            fundRegistration.Property(x => x.CreatedAt).IsRequired();
            fundRegistration.Property(x => x.SucceededAt).IsRequired(false);
        }
    }
}
