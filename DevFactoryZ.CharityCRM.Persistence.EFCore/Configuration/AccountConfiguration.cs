using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Configuration
{
    internal class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> account)
        {
            account.ToTable(nameof(Account));

            account.HasKey(x => x.Id);

            account.Property(x => x.Id).ValueGeneratedOnAdd();

            account.Property(x => x.Login)                
                .HasMaxLength(Account.LoginMaxLength)
                .IsRequired(Account.LoginIsRequired);

            account.Property(x => x.CreatedAt).IsRequired();

            #region Конфигурация связи с AccountSession
            
            account.HasMany<AccountSession>()
                .WithOne(a => a.Account)
                .HasForeignKey("AccountId")
                .IsRequired();

            #endregion

            #region Для связи с Password и PasswordConfig

            account.OwnsOne(a => a.Password, password =>
                {
                    password.WithOwner();
                    password.Ignore(p => p.PasswordConfig);
                    password.Ignore(p => p.TemporaryPassword);
                    password.Ignore(p => p.Salt);
                    password.Ignore(p => p.Hash);
                    password.Ignore(p => p.IsAlive);
                   

                    password.HasOne(p => p.PasswordConfig)
                        .WithMany()
                        .IsRequired()
                        .HasForeignKey("PasswordConfigId")                        
                        .OnDelete(DeleteBehavior.Restrict);                    


                    password.Property(p => p.RawHash).IsRequired();

                    password.Property(p => p.RawSalt).IsRequired();

                    password.ToTable(nameof(Account));
                });

            #endregion
        }
    }
}
