using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Configuration
{
    class PasswordConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> account)
        {
            account.OwnsOne(a => a.Password, password =>
            {
                password.ToTable(nameof(Account));

                password.Property(p => p.RawHash).IsRequired();
                password.Property(p => p.RawSalt).IsRequired();

                password.Ignore(p => p.PasswordConfig);
                password.Ignore(p => p.TemporaryPassword);
                password.Ignore(p => p.Salt);
                password.Ignore(p => p.Hash);
                password.Ignore(p => p.IsAlive);

                #region Конфигурация связи Passwords с PasswordConfig

                password.HasOne(p => p.PasswordConfig)
                    .WithMany()
                    .IsRequired()
                    .HasForeignKey("PasswordConfigId")
                    .OnDelete(DeleteBehavior.Restrict);

                #endregion
            });
        }
    }
}
