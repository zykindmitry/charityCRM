using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Configuration
{
    class PasswordConfigConfiguration : IEntityTypeConfiguration<PasswordConfig>
    {
        public void Configure(EntityTypeBuilder<PasswordConfig> passwordConfig)
        {
            passwordConfig.ToTable(nameof(PasswordConfig));

            passwordConfig.HasKey(p => p.Id);

            passwordConfig.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            passwordConfig.Property(p => p.MaxLifeTime)
                .HasConversion(
                    v => v.Days,
                    v => TimeSpan.FromDays(v))
                .IsRequired();

            passwordConfig.Property(p => p.MinLength)
                .IsRequired();
            
            passwordConfig.Property(p => p.SaltLength)
                .IsRequired();
            
            passwordConfig.Property(p => p.UseDigits)
                .IsRequired();
            
            passwordConfig.Property(p => p.UseUpperCase)
                .IsRequired();
            
            passwordConfig.Property(p => p.UseSpecialSymbols)
                .IsRequired();
            
            passwordConfig.Property(p => p.SpecialSymbols)
                .HasConversion(
                    v => string.Join(string.Empty, v),
                    v => v.ToCharArray())
                .IsRequired();
                
            passwordConfig.Property(p => p.CreatedAt)
                .IsRequired();
        }
    }
}
