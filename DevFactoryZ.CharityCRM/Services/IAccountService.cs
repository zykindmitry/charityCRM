using DevFactoryZ.CharityCRM.Persistence;
using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.Services
{
    /// <summary>
    /// Описывает бизнес транзакции над доменным обектом <see cref="Account"/>.
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
        /// Получение из хранилища объекта <see cref="Account"/> по заданному <see cref="Account.Login"/>.
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
        /// Сохранение нового объекта <see cref="Account"/> в хранилище.
        /// </summary>
        /// <param name="accountData">Данные  для сохранения.</param>
        /// <returns>Сохраненный <see cref="Account"/>.</returns>
        Account Create(AccountData accountData);

        /// <summary>
        /// Изменение пароля для существующего объекта <see cref="Account"/> в хранилище.
        /// Генерирует <see cref="EntityNotFoundException"/>, если объект не найден.
        /// </summary>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <param name="login">Логин пользователя, мененяющего пароль.</param>
        /// <param name="newPassword">Новый пароль.</param>
        /// <param name="actualPasswordConfig">Актуальная конфигурация сложности пароля.</param>
        void Update(string login, char[] newPassword, IPasswordConfig actualPasswordConfig);

        /// <summary>
        /// Изменение пароля для существующего объекта <see cref="Account"/> в хранилище.
        /// Генерирует <see cref="EntityNotFoundException"/>, если объект не найден.
        /// </summary>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <param name="id">Идентификатор объекта <see cref="Account"/> в хранилище.</param>
        /// <param name="accountData">Данные для изменения пароля.</param>
        void Update(int id, AccountData accountData);

        /// <summary>
        /// Удаление объекта <see cref="Account"/> из хранилища.
        /// </summary>
        /// <param name="id">Идентификатор аккаунта для удаления.</param>
        void Delete(int id);

        /// <summary>
        /// Сохранение нового объекта <see cref="AccountSession"/> в хранилище.  
        /// </summary>
        /// <param name="login">Логин пользователя из Http-запроса на аутентификацию, для которого создается <see cref="AccountSession"/>.</param>
        /// <param name="password">Пароль пользователя из Http-запроса на аутентификацию, для которого создается <see cref="AccountSession"/>.</param>
        /// <param name="userAgent">User-Agent из Http-запроса на аутентификацию, для которого создается <see cref="AccountSession"/>.</param>
        /// <param name="ipAddress">IP-адрес из Http-запроса на аутентификацию, для которого создается <see cref="AccountSession"/>.</param>
        /// <returns>Экзампляр <see cref="AccountSession"/> для пользователя, прошедшего аутентификацию по логину и паролю.</returns>
        AccountSession Login(string login, string password, string userAgent, string ipAddress);
    }
}
