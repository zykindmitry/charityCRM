﻿using Microsoft.EntityFrameworkCore;
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
        }
    }
}
