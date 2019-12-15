using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.Persistence
{
    public interface IRepository<TEntity, TKey> 
        where TEntity : IAmPersistent<TKey>
        where TKey : struct
    {
        /// <summary>
        /// Возвращает все сущности типа TEntity в системе
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Помечает объект типа TEntity на удаление в unit of work
        /// см. Save
        /// </summary>
        /// <param name="id">Идентификатор объекта обобщенного типа TKey</param>
        void Delete(TKey id);

        /// <summary>
        /// Добавляет объект типа TEntity в unit of work для последующей вставки в хранилище данных системы
        /// см. Save
        /// </summary>
        /// <param name="repositoryType"></param>
        void Create(TEntity repositoryType);

        /// <summary>
        /// Возвращает экземпляр класса TEntity, если он найден по его id
        /// </summary>
        /// <param name="id"></param>
        TEntity GetById(TKey id);

        /// <summary>
        /// Сохраняет все new и dirty объекты в хранилище данных и перерегистрирует их как clean
        /// </summary>
        void Save();
    }
}
