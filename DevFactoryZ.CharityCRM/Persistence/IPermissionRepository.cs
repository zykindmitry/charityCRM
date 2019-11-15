using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.Persistence
{
    /// <summary>
    /// Интерфейс описывает шаблон репозиторий для разрешений
    /// </summary>
    public interface IPermissionRepository
    {
        /// <summary>
        /// Возвращает все разрешения в системе
        /// </summary>
        /// <returns></returns>
        IEnumerable<Permission> GetAll();

        /// <summary>
        /// Помечает разрешение на удаление в IUnitOfWork
        /// см. IUnitOfWorkSave
        /// </summary>
        /// <param name="id">Идентификатор разрешения, который необходимо удалить</param>
        void Delete(int id);

        /// <summary>
        /// Добавляет разрешение в IUnitOfWork для последующей вставки в хранилище данных системы
        /// см. IUnitOfWork.Save
        /// </summary>
        /// <param name="permission">Новое разрешение</param>
        void Create(Permission permission);
    }
}
