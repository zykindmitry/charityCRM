using DevFactoryZ.CharityCRM.Persistence;
using System.Collections.Generic;
using System;

namespace DevFactoryZ.CharityCRM.Services
{
    /// <summary>
    /// Описывает CRUD-методы управления объектами типа <see cref="AccountSession"/>.
    /// </summary>
    public interface IAccountSessionService
    {
        /// <summary>
        /// Получение всех <see cref="AccountSession"/>'s из хранилища.
        /// </summary>
        /// <returns>Коллекция <see cref="AccountSession"/>'s.</returns>
        IEnumerable<AccountSession> GetAll();

        /// <summary>
        /// Получение из хранилища объекта <see cref="AccountSession"/> по заданному идентификатору <see cref="AccountSession.Id"/>.
        /// Генерирует <see cref="EntityNotFoundException"/>, если объект не найден.
        /// </summary>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <param name="id">Идентификатор объекта <see cref="AccountSession"/> в хранилище.</param>
        /// <returns>Найденный в хранилище объект <see cref="AccountSession"/>.</returns>
        AccountSession GetById(Guid id);

        /// <summary>
        /// Удаление объекта <see cref="AccountSession"/> из хранилища.
        /// </summary>
        /// <param name="id">Идентификатор объекта <see cref="AccountSession"/> для удаления.</param>
        void Delete(Guid id);
    }
}
