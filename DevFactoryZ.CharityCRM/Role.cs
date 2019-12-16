using System;
using System.Collections.Generic;
using System.Linq;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Этот класс представляет роль пользователя или именованный набор разрешений
    /// </summary>
    public class Role : IAmPersistent<int>
    {
        #region .ctor

        protected Role() // for ORM 
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
            
            permissions.Each(permission => Grant(permission));
        }

        #endregion

        #region Хранение и идентификация

        /// <summary>
        /// Возвращает идентификатор роли в хранилище
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// Возвращает признак доступности цдаления разрешения из хранилища данных системы
        /// </summary>
        public bool CanBeDeleted => true;

        #endregion

        #region Описание роли

        public static bool NameIsRequired = true;

        public static int NameMaxLength = 25;

        private readonly RealString name =
            new RealString(NameMaxLength, NameIsRequired, "наименование роли");

        /// <summary>
        /// Возвращает или задает имя роли
        /// </summary>
        public string Name { get => name.Value; set => name.Value = value; }

        /// <summary>
        /// Возвращает или задает описание роли
        /// </summary>
        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region Разрешения

        public class RolePermission
        {
            protected RolePermission() // for ORM
            {
            }

            internal RolePermission(Permission permission)
            {
                Permission = permission;
            }

            public Permission Permission { get; protected set; }
        }

        public static string RolePermissionsFieldName => nameof(permissions);

        private readonly List<RolePermission> permissions = new List<RolePermission>();

        /// <summary>
        /// Возвращает коллекцию разрешений роли
        /// </summary>
        public IEnumerable<RolePermission> Permissions => permissions.AsReadOnly();

        /// <summary>
        /// Выдает разрешение пользователям в данной роли
        /// </summary>
        /// <param name="permission">Разрешение, которое нужно выдать роли</param>
        public void Grant(Permission permission)
        {
            if (permissions
                .Any(x => x.Permission.Equals(PermissionOrNullException(permission))))
            {
                throw new InvalidOperationException(
                    $"Роли {this} уже доступно разрешение {permission}");
            }

            permissions.Add(new RolePermission(permission));
        }

        /// <summary>
        /// Отозвать разрешение, выданное данным пользователям
        /// </summary>
        /// <param name="permission">Разрешение. которое необходимо отозвать</param>
        public void Deny(Permission permission)
        {
            var rolePermission = permissions
                .FirstOrDefault(x => x.Permission.Equals(PermissionOrNullException(permission)));

            if (rolePermission == null)
            {
                throw new InvalidOperationException($"Роль {this} не имеет разрешения {permission}");
            }

            permissions.Remove(rolePermission);
        }

        private Permission PermissionOrNullException(Permission permission)
        {
            return permission ?? throw new ArgumentNullException(nameof(permission));
        }

        #endregion
    }
}
