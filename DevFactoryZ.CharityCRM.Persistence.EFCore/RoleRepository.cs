using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore
{
    internal class RoleRepository : IRoleRepository
    {
        private readonly DbSet<Role> setOfRoles;

        private readonly Action save;

        public RoleRepository(DbSet<Role> setOfRoles, Action save)
        {
            this.setOfRoles = setOfRoles;
            this.save = save;
        }

        public void Add(Role role)
        {
            setOfRoles.Add(role);
        }

        public void Delete(int id)
        {
            setOfRoles.Remove(GetById(id));
        }

        public IEnumerable<Role> GetAll()
        {
            return setOfRoles.ToArray();
        }

        public Role GetById(int id)
        {
            return setOfRoles
                .Include(r => r.Permissions)
                    .ThenInclude(x => x.Permission)                
                .FirstOrDefault(r => r.Id == id)
                ?? throw new EntityNotFoundException(id, typeof(Role));
        }

        public void Save()
        {
            save();
        }
    }
}
