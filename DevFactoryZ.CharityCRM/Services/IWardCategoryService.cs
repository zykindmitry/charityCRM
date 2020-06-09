using DevFactoryZ.CharityCRM.Persistence;
using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.Services
{
    /// <summary>
    /// Описывает CRUD-методы и методы добавления/удаления подкатегорий 
    /// <see cref="WardCategory"/>, для категории <see cref="WardCategory"/>.
    /// </summary>
    public interface IWardCategoryService
    {
        /// <summary>
        /// Получение всех <see cref="WardCategory"/>'s из хранилища.
        /// </summary>
        /// <returns>Коллекция <see cref="WardCategory"/>'s.</returns>
        IEnumerable<WardCategory> GetAll();

        /// <summary>
        /// Получение из хранилища объекта <see cref="WardCategory"/> 
        /// по заданному идентификатору <see cref="WardCategory.Id"/>.
        /// Генерирует <see cref="EntityNotFoundException"/>, если объект не найден.
        /// </summary>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <param name="id">Идентификатор категории в хранилище.</param>
        /// <returns>Найденный в хранилище объект <see cref="WardCategory"/>.</returns>
        WardCategory GetById(int id);

        /// <summary>
        /// Сохранение нового объекта <see cref="WardCategory"/> в хранилище.
        /// </summary>
        /// <param name="wardCategory">Данные для сохранения.</param>
        /// <returns>Сохраненная <see cref="WardCategory"/>.</returns>
        WardCategory Create(WardCategory wardCategory);

        /// <summary>
        /// Изменение названия существующего объекта <see cref="WardCategory"/> в хранилище.
        /// Генерирует <see cref="EntityNotFoundException"/>, если объект не найден.
        /// </summary>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <param name="id">Идентификатор изменяемой категории в хранилище.</param>
        /// <param name="newName">Новое название категории.</param>
        void Update(int id, string newName);

        /// <summary>
        /// Удаление объекта <see cref="WardCategory"/> из хранилища.
        /// </summary>
        /// <param name="id">Идентификатор категории для удаления.</param>
        void Delete(int id);

        /// <summary>
        /// Добавляет <see cref="WardCategory"/> в список подкатегорий для 
        /// <see cref="WardCategory"/>.
        /// </summary>
        /// <param name="parentId">Идентификатор родительской категории в хранилище.</param>
        /// <param name="childId">Идентификатор подкатегории <see cref="WardCategory"/> для добавления.</param>
        void AddChild(int parentId, int childId);

        /// <summary>
        /// Удаляет <see cref="WardCategory"/> из списка подкатегорий для 
        /// <see cref="WardCategory"/>.
        /// </summary>
        /// <param name="parentId">Идентификатор родительской категории в хранилище.</param>
        /// <param name="childId">Идентификатор подкатегории <see cref="WardCategory"/> для удаления.</param>
        void RemoveChild(int parentId, int childId);

        /// <summary>
        /// Проверка наличия родительской <see cref="WardCategory"/> у проверяемой 
        /// <see cref="WardCategory"/>.
        /// </summary>
        /// <param name="wardCategoryId">Идентификатор проверяемой <see cref="WardCategory"/>.</param>
        /// <returns>Результат проверки</returns>
        bool HasParent(int wardCategoryId);

        /// <summary>
        /// Проверка наличия циклической зависимости между категориями <see cref="WardCategory"/>.
        /// </summary>
        /// <param name="parentId">Идентификатор родительской <see cref="WardCategory"/>.</param>
        /// <param name="childId">Идентификатор дочерней <see cref="WardCategory"/>.</param>
        /// <returns></returns>
        bool HasCycling(int parentId, int childId);
    }
}
