using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Configuration
{
    internal class RolePermissionConfiguration : IEntityTypeConfiguration<Role.RolePermission>
    {
        public void Configure(EntityTypeBuilder<Role.RolePermission> rolePermission)
        {
            rolePermission.ToTable("RolePermission").HasKey("RoleId", "PermissionId");
            rolePermission.HasOne(x => x.Permission).WithMany().HasForeignKey("PermissionId");
        }
    }
}
