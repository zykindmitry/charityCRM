using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore
{
    internal class PermissionRepository : IPermissionRepository
    {
        private readonly DbSet<Permission> setOfPermissions;

        public PermissionRepository(DbSet<Permission> setOfPermissions)
        {
            this.setOfPermissions = setOfPermissions;
        }

        public void Create(Permission permission)
        {
            setOfPermissions.Add(permission);
        }

        public void Delete(int id)
        {
            setOfPermissions.Find(id);
        }

        public IEnumerable<Permission> GetAll()
        {
            return setOfPermissions.ToArray();
        }
    }
}
