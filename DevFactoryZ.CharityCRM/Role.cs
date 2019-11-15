using System;
using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Этот класс представляет роль пользователя или именованный набор разрешений
    /// </summary>
    public class Role : IAmPersistent<int>
    {
        protected Role()
        {
        }

        /// <summary>
        /// Создает экземпляр класса DevFactoryZ.CharityCRM.Role
        /// </summary>
        /// <param name="name">Имя роли</param>
        /// <param name="description">Описание роли</param>
        /// <param name="permissions">Набор разрешения, предоставляемый пользователю в данной роли</param>
        public Role(string name, string description, IEnumerable<Permission> permissions) : this()
        {
            Name = 
                !string.IsNullOrWhiteSpace(name) 
                    ? name 
                    : throw new ArgumentException("Имя роли не может быть пустым", nameof(name));
            Description = description;
            permissions.Each(permission => this.permissions.Add(permission));
        }

        /// <summary>
        /// Возвращает идентификатор роли в хранилище
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// Возвращает признак доступности цдаления разрешения из хранилища данных системы
        /// </summary>
        public bool CanBeDeleted => true;

        /// <summary>
        /// Возвращает или задает имя роли
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Возвращает или задает описание роли
        /// </summary>
        public string Description { get; set; }

        private readonly HashSet<Permission> permissions = new HashSet<Permission>();

        /// <summary>
        /// Возвращает коллекцию разрешений роли
        /// </summary>
        public ICollection<Permission> Permissions => permissions;
    }
}
