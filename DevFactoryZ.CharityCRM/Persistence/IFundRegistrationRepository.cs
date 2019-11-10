using System;
using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.Persistence
{
    /// <summary>
    /// Интерфейс взаимодействия с хранилищем.
    /// </summary>
    public interface IFundRegistrationRepository
    {
        /// <summary>
        /// Создание новой записи для объекта типа FundRegistration в хранилище.
        /// </summary>
        /// <param name="entity">Объект, для которого будет создана новая запись в хранилище.</param>
        /// <returns>Primary key созданной записи в хранилище.</returns>
        int Create(FundRegistration entity);

        /// <summary>
        /// Получение объекта типа FundRegistration из хранилища по заданному primary key.
        /// </summary>
        /// <param name="id">Значение primary key для поиска нужной записи.</param>
        /// <returns>Найденный в хранилище объект типа FundRegistration.</returns>
        FundRegistration GetById(int id);

        /// <summary>
        /// Получение объекта типа FundRegistration из хранилища по заданному значению поля registrationLinkGUID.
        /// </summary>
        /// <param name="registrationLinkGUID">Значение поля registrationLinkGUID для поиска нужной записи.</param>
        /// <returns>Найденный в хранилище объект типа FundRegistration.</returns>
        FundRegistration GetByGuid(Guid registrationLinkGUID);

        /// <summary>
        /// Получение всех объектов типа FundRegistration из хранилища. 
        /// </summary>
        /// <returns>Все найденные в хранилище объекты типа FundRegistration.</returns>
        IEnumerable<FundRegistration> GetAll();

        /// <summary>
        /// Удаление из хранилища записи по заданному primary key.
        /// </summary>
        /// <param name="id">Primary key записи, которую надо удалить.</param>
        void Delete(int id);
    }
}
