using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.Services
{
    public class PermissionData
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public interface IPermissionService
    {
        IEnumerable<Permission> GetAll();

        Permission GetById(int id);

        Permission Create(PermissionData data);

        void Update(int id, PermissionData data);

        void Delete(int id);
    }
}
