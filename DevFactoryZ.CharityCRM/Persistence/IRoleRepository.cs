using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.Persistence
{
    /// <summary>
    /// Интерфейс описывает шаблон репозиторий для разрешений
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// Возвращает все роли в системе
        /// </summary>
        /// <returns></returns>
        IEnumerable<Role> GetAll();

        /// <summary>
        /// Помечает роль на удаление в IUnitOfWork
        /// см. IUnitOfWorkSave
        /// </summary>
        /// <param name="id">Идентификатор роли, который необходимо удалить</param>
        void Delete(int id);

        /// <summary>
        /// Добавляет роль в IUnitOfWork для последующей вставки в хранилище данных системы
        /// см. IUnitOfWork.Save
        /// </summary>
        /// <param name="role">Новая роль</param>
        void Create(Role role);
    }
}
