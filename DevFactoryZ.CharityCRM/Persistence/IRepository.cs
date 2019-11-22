using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.Persistence
{
    public interface IRepository<TEntity, TKey>
    {
        /// <summary>
        /// Возвращает все сущности типа TEntity в системе
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Помечает объект типа TEntity на удаление в IUnitOfWork
        /// см. IUnitOfWorkSave
        /// </summary>
        /// <param name="id">Идентификатор объекта обобщенного типа TKey</param>
        void Delete(TKey id);

        /// <summary>
        /// Добавляет объект типа TEntity в IUnitOfWork для последующей вставки в хранилище данных системы
        /// см. IUnitOfWork.Save
        /// </summary>
        /// <param name="repositoryType"></param>
        void Create(TEntity repositoryType);

        /// <summary>
        /// Возвращает экземпляр класса TEntity, если он найден по его id
        /// </summary>
        /// <param name="id"></param>
        TEntity GetById(TKey id);
    }
}
