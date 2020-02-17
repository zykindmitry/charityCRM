using System.Collections.Generic;
using DevFactoryZ.CharityCRM.Persistence;

namespace DevFactoryZ.CharityCRM.Services
{
    /// <summary>
    /// Вспомогательный тип для передачи значений свойств типу <see cref="Role"/> при создании/изменении роли.
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
        /// Коллекция разрешений <see cref="Permission"/>'s для роли.
        /// </summary>
        public IEnumerable<Permission> Permissions { get; set; }
    }

    /// <summary>
    /// Описывает CRUD-методы и методы добавления/удаления разрешений <see cref="Permission"/> для <see cref="Role"/>.
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Получение всех <see cref="Role"/>'s из хранилища.
        /// </summary>
        /// <returns>Коллекция <see cref="Role"/>'s.</returns>
        IEnumerable<Role> GetAll();

        /// <summary>
        /// Получение из хранилища объекта <see cref="Role"/> по заданному идентификатору <see cref="Role.Id"/>.
        /// Генерирует <see cref="EntityNotFoundException"/>, если объект не найден.
        /// </summary>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <param name="id">Идентификатор роли в хранилище.</param>
        /// <returns>Найденный в хранилище объект <see cref="Role"/>.</returns>
        Role GetById(int id);

        /// <summary>
        /// Сохранение нового объекта <see cref="Role"/> в хранилище.
        /// </summary>
        /// <param name="data">Данные роли для сохранения.</param>
        /// <returns>Сохраненная <see cref="Role"/>.</returns>
        Role Create(RoleData data);

        /// <summary>
        /// Изменение существующего объекта <see cref="Role"/> в хранилище.
        /// Генерирует <see cref="EntityNotFoundException"/>, если объект не найден.
        /// </summary>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <param name="id">Идентификатор роли в хранилище.</param>
        /// <param name="data">Данные роли для изменения.</param>
        void Update(int id, RoleData data);

        /// <summary>
        /// Удаление объекта <see cref="Role"/> из хранилища.
        /// </summary>
        /// <param name="id">Идентификатор роли для удаления.</param>
        void Delete(int id);

        /// <summary>
        /// Добавляет <see cref="Permission"/> в список разрешений для <see cref="Role"/>.
        /// </summary>
        /// <param name="id">Идентификатор роли в хранилище.</param>
        /// <param name="permission"><see cref="Permission"/> для добавления.</param>
        void Grant(int id, Permission permission);

        /// <summary>
        /// Удаляет <see cref="Permission"/> из списка разрешений для <see cref="Role"/>.
        /// </summary>
        /// <param name="id">Идентификатор роли в хранилище.</param>
        /// <param name="permission"><see cref="Permission"/> для удаления.</param>
        void Deny(int id, Permission permission);
    }
}
