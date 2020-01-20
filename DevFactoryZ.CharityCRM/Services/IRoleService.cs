using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.Services
{
    /// <summary>
    /// Вспомогательный тип для передачи значений свойств роли типу 'Role' при создании/изменении роли.
    /// </summary>
    public class RoleData
    {
        /// <summary>
        /// Наименование роли.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание роли.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Коллекция разрешений для роли.
        /// </summary>
        public IEnumerable<Permission> Permissions { get; set; }
    }

    /// <summary>
    /// Описывает CRUD-методы и методы добавления/удаления разрешений (тип 'Permission') для роли (тип 'Role').
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Получение всех ролей из хранилища.
        /// </summary>
        /// <returns>Коллекция ролей.</returns>
        IEnumerable<Role> GetAll();

        /// <summary>
        /// Получение из хранилища роли по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор роли в хранилище.</param>
        /// <returns>Роль.</returns>
        Role GetById(int id);

        /// <summary>
        /// Сохранение новой роли в хранилище.
        /// </summary>
        /// <param name="data">Данные роли для сохранения.</param>
        /// <returns>Сохраненная роль.</returns>
        Role Create(RoleData data);

        /// <summary>
        /// Изменение существующей роли в хранилище.
        /// </summary>
        /// <param name="id">Идентификатор роли в хранилище.</param>
        /// <param name="data">Данные роли для изменения.</param>
        void Update(int id, RoleData data);

        /// <summary>
        /// Удаление рои из хранилища.
        /// </summary>
        /// <param name="id">Данные роли для удаления.</param>
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
