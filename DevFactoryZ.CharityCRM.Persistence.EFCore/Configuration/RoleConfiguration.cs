using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Configuration
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> role)
        {
            role
                .HasKey(x => x.Id);
            
            role
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            role
                .Property(x => x.Name)
                .HasMaxLength(Role.NameMaxLength)
                .IsRequired(Role.NameIsRequired);

            role
                .HasMany(x => x.Permissions)
                .WithOne()
                .HasForeignKey("RoleId");

            var permissionNavigation = role.Metadata.FindNavigation(nameof(Role.Permissions));
            permissionNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
            permissionNavigation.SetField(Role.RolePermissionsFieldName);
        }
    }
}
