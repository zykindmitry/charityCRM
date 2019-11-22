using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.Persistence
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Создание новой записи для объекта типа Account в хранилище.
        /// </summary>
        /// <param name="entity">Объект, для которого будет создана новая запись в хранилище.</param>
        /// <returns>Primary key созданной записи в хранилище.</returns>
        int Create(Account entity);

        /// <summary>
        /// Получение объекта типа Account из хранилища по заданному primary key.
        /// </summary>
        /// <param name="id">Значение primary key для поиска нужной записи.</param>
        /// <returns>Найденный в хранилище объект типа Account.</returns>
        Account GetById(int id);

        /// <summary>
        /// Получение объекта типа Account из хранилища по заданному Login.
        /// </summary>
        /// <param name="id">Значение Login для поиска нужной записи.</param>
        /// <returns>Найденный в хранилище объект типа Account.</returns>
        Account GetByLogin(string login);

        /// <summary>
        /// Получение всех объектов типа Account из хранилища. 
        /// </summary>
        /// <returns>Все найденные в хранилище объекты типа Account.</returns>
        IEnumerable<Account> GetAll();

        /// <summary>
        /// Удаление из хранилища записи по заданному primary key.
        /// </summary>
        /// <param name="id">Primary key записи, которую надо удалить.</param>
        void Delete(int id);
    }
}
