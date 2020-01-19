using DevFactoryZ.CharityCRM.Persistence;
using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository repository;

        public RoleService(IRoleRepository repository)
        {
            this.repository = repository;
        }

        public Role Create(RoleData data)
        {
            var role = new Role(data.Name, data.Description, data.Permissions);
            repository.Create(role);
            repository.Save();

            return role;
        }

        public void Delete(int id)
        {
            repository.Delete(id);
            repository.Save();
        }

        public void Deny(int id, Permission permission)
        {
            var role = repository.GetById(id);
            role.Deny(permission);

            repository.Save();
        }

        public IEnumerable<Role> GetAll()
        {
            return repository.GetAll();
        }

        public Role GetById(int id)
        {
            return repository.GetById(id);
        }

        public void Grant(int id, Permission permission)
        {
            var role = repository.GetById(id);
            role.Grant(permission);

            repository.Save();
        }

        public void Update(int id, RoleData data)
        {
            var role = repository.GetById(id);
            role.Name = data.Name;
            role.Description = data.Description;
            
            repository.Save();
        }
    }
}
