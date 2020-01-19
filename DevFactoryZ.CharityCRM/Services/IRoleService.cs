using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.Services
{
    public class RoleData
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<Permission> Permissions { get; set; }
}

    public interface IRoleService
    {
        IEnumerable<Role> GetAll();

        Role GetById(int id);

        Role Create(RoleData data);

        void Update(int id, RoleData data);

        void Delete(int id);

        /// <summary>
        /// Добавляет разрешение в список разрешений для роли.
        /// </summary>
        /// <param name="id">Идентификатор роли.</param>
        /// <param name="permission">Разрешение для добавления.</param>
        void Grant(int id, Permission permission);

        /// <summary>
        /// Удаляет разрешение из списка разрешений для роли.
        /// </summary>
        /// <param name="id">Идентификатор роли.</param>
        /// <param name="permission">Разрешение для удаления.</param>
        void Deny(int id, Permission permission);
    }
}
