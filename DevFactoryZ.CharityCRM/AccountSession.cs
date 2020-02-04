using System;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Содержит данные о пользовательской сессии.
    /// </summary>
    public class AccountSession : IAmPersistent<Guid>
    {
        #region .ctor

        /// <summary>
        /// Для создания миграции.
        /// </summary>
        protected AccountSession()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Создает экземпляр пользовательской сессии.
        /// </summary>
        /// <param name="account">Аккаунт пользователя, инициирующего новую сессию.</param>
        /// <param name="userAgent">User-Agent из запроса.</param>
        /// <param name="ipAddress">IP-адрес, с которого происходит обращение к приложению.</param>
        /// <param name="expiredAt">Дата/время, после которого пользовательская сессия становится невалидной.</param>
        public AccountSession(
            Account account
            , string userAgent
            , string ipAddress
            , DateTime expiredAt)
            : this()
        {
            Account = account;
            UserAgent = userAgent;
            IPAddress = ipAddress;
            ExpiredAt = expiredAt;
        }

        #endregion

        #region Данные сессии

        /// <summary>
        /// Возвращает уникальный идентификатор учетной записи.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Возвращает признак доступности удаления <see cref="AccountSession"/> из хранилища данных системы.
        /// </summary>
        public bool CanBeDeleted => false;

        /// <summary>
        /// Родительский <see cref="Account"/>
        /// </summary>
        public Account Account { get; }

        /// <summary>
        /// User-Agent из первого HTTP-запроса в текущей сессии.
        /// </summary>
        public string UserAgent { get; }

        /// <summary>
        /// IP-адрес  из первого HTTP-запроса в текущей сессии.
        /// </summary>
        public string IPAddress { get; }

        /// <summary>
        /// Время окончания срока жизни пользовательской сессии в формате UTC.
        /// </summary>
        public DateTime ExpiredAt { get; private set; }

        #endregion

        /// <summary>
        /// Продление срока жизни пользовательской сессии <see cref="AccountSession"/> на указанный интервал времени.
        /// </summary>
        /// <param name="newTimeSpan">Интервал времени, на который продляется срок жизни пользовательской сессии <see cref="AccountSession"/></param>
        public void ExtendSession(TimeSpan newTimeSpan)
        {
            ExpiredAt = DateTime.UtcNow.Add(newTimeSpan);
        } 
    }
}
