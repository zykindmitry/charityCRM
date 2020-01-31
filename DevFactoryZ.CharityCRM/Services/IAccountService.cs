using DevFactoryZ.CharityCRM.Persistence;
using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.Services
{
    /// <summary>
    /// Описывает CRUD-методы управления объектами типа <see cref="Account"/>.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Получение всех <see cref="Account"/>'s из хранилища.
        /// </summary>
        /// <returns>Коллекция <see cref="Account"/>'s.</returns>
        IEnumerable<Account> GetAll();

        /// <summary>
        /// Получение из хранилища объекта <see cref="Account"/> по заданному идентификатору <see cref="Account.Id"/>.
        /// Генерирует <see cref="EntityNotFoundException"/>, если объект не найден.
        /// </summary>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <param name="id">Идентификатор аккаунта в хранилище.</param>
        /// <returns>Найденный в хранилище объект <see cref="Account"/>.</returns>
        Account GetById(int id);

        /// <summary>
        /// Возвращает объект типа <see cref="Account"/> из хранилища по заданному <see cref="Account.Login"/>.
        /// Генерирует <see cref="EntityNotFoundException"/>, если объект не найден.
        /// </summary>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <param name="login">Значение <see cref="Account.Login"/> для поиска нужной записи.</param>
        /// <returns>Найденный в хранилище объект <see cref="Account"/>.</returns>
        Account GetByLogin(string login);

        /// <summary>
        /// Сохранение нового объекта <see cref="Account"/> в хранилище.
        /// </summary>
        /// <param name="newAccount">Данные  для сохранения.</param>
        /// <returns>Сохраненный <see cref="Account"/>.</returns>
        Account Create(Account newAccount);

        /// <summary>
        /// Изменение пароля для существующего объекта <see cref="Account"/> в хранилище.
        /// Генерирует <see cref="EntityNotFoundException"/>, если объект не найден.
        /// </summary>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <param name="login">Логин пользователя, мененяющего пароль.</param>
        /// <param name="newPassword">Новый пароль.</param>
        void Update(string login, char[] newPassword);

        /// <summary>
        /// Удаление объекта <see cref="Account"/> из хранилища.
        /// </summary>
        /// <param name="id">Идентификатор аккаунта для удаления.</param>
        void Delete(int id);
    }
}
