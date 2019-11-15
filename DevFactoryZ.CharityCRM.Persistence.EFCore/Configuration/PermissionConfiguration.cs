using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Configuration
{
    internal class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> permission)
        {
            permission.HasKey(x => x.Id);
            permission.Property(x => x.Id).ValueGeneratedOnAdd();

            permission.Property(x => x.Name).HasMaxLength(100);
        }
    }
}
