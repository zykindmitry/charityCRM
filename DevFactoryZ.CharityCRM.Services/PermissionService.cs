using DevFactoryZ.CharityCRM.Persistence;
using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository repository;

        public PermissionService(IPermissionRepository repository)
        {
            this.repository = repository;
        }

        public Permission Create(PermissionData data)
        {
            var permission = new Permission(data.Name, data.Description);
            repository.Add(permission);
            repository.Save();

            return permission;
        }

        public void Delete(int id)
        {
            repository.Delete(id);
            repository.Save();
        }

        public IEnumerable<Permission> GetAll()
        {
            return repository.GetAll();
        }

        public Permission GetById(int id)
        {
            return repository.GetById(id);
        }

        public void Update(int id, PermissionData data)
        {
            var permission = repository.GetById(id);
            permission.Name = data.Name;
            permission.Description = data.Description;

            repository.Save();
        }
    }
}
