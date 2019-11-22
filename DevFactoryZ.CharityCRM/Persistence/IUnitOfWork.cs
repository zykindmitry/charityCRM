using System;

namespace DevFactoryZ.CharityCRM.Persistence
{
    /// <summary>
    /// Этот интерфейс описывает методы шаблона unit of work
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Сохраняет все накопленные изменения в хранилище данных       
        /// </summary>
        void Save();

        /// <summary>
        /// Добавляет новую сущность для последующей вставки в хранилище данных
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности</typeparam>
        /// <param name="newEntity">Ссылка на объект, который надо добавить</param>
        void Add<TEntity>(TEntity newEntity) where TEntity : class;

        /// <summary>
        /// Помечает объект с определенным значением ключа для последующего удаления
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности</typeparam>
        /// <param name="entityToRemove">Объект-сущность. который необходимо удалить</param>
        void Remove<TEntity>(TEntity entityToRemove) where TEntity : class;

        /// <summary>
        /// Ищет объект по заданному значению ключа в хранилище
        /// Генерирует EntityNotFoundException если объект не найден
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности</typeparam>
        /// <typeparam name="TKey">Тип ключа</typeparam>
        /// <param name="id">Значение ключа</param>
        /// <returns>Результат поиска</returns>
        TEntity GetById<TEntity, TKey>(TKey id)
            where TEntity : class, IAmPersistent<TKey>
            where TKey : struct;
    }
}
