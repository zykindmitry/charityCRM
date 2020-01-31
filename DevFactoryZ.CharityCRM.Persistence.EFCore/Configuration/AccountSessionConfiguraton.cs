using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Configuration
{
    internal class AccountSessionConfiguraton : IEntityTypeConfiguration<AccountSession>
    {
        public void Configure(EntityTypeBuilder<AccountSession> accountSession)
        {
            accountSession.HasKey(x => x.Id);

            accountSession.Property(x => x.Id);

            accountSession.Property(x => x.UserAgent)
                .IsRequired();

            accountSession.Property(x => x.IPAddress)
                .IsRequired();

            accountSession.Property(x => x.ExpiredAt)
                .IsRequired();
        }
    }
}
