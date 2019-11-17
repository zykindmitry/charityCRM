using System;
using System.Collections.Generic;
using System.Text;

namespace DevFactoryZ.CharityCRM.Persistence
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Возвращает все сущности типа T в системе
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Помечает объект типа T на удаление в IUnitOfWork
        /// см. IUnitOfWorkSave
        /// </summary>
        /// <param name="id">Идентификатор объекта типа T, который необходимо удалить</param>
        void Delete(int id);

        /// <summary>
        /// Добавляет объект типа T в IUnitOfWork для последующей вставки в хранилище данных системы
        /// см. IUnitOfWork.Save
        /// </summary>
        /// <param name="repositoryType">Реализация типа T</param>
        void Create(T repositoryType);
    }
}
