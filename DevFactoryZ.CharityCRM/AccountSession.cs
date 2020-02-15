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

        #region Проверки валидности сессии

        /// <summary>
        /// Проверяет подлинность HTTP-запроса путем сравнения IP-адреса клиента и User-Agent из HTTP-запроса со значениями, 
        /// хранящимися в <see cref="AccountSession"/> на сервере.
        /// </summary>
        /// <param name="userAgent">User-Agent из HTTP-запроса.</param>
        /// <param name="ipAddress">IP-адреса клиента из HTTP-запроса.</param>
        /// <returns>Результат сравнения.</returns>
        public bool IsReliable(string userAgent, string ipAddress)
        {
            return UserAgent.Equals(userAgent, StringComparison.InvariantCulture)
                && IPAddress.Equals(ipAddress, StringComparison.InvariantCulture);
        }

        /// <summary>
        /// Проверяет актуальность <see cref="AccountSession"/> путем сравнения времени окончания срока жизни, хранящегося в текущем <see cref="AccountSession"/>, 
        /// с текущим временем на сервере.
        /// </summary>
        /// <returns>Результат проверки.</returns>
        public bool IsAlive()
        {
            return ExpiredAt >= DateTime.UtcNow;
        }

        /// <summary>
        /// Аутентификация пользователя по паролю с использованием метода <see cref="Account.Authenticate(char[])"/>.
        /// </summary>
        /// <exception cref="ValidationException"></exception>
        /// <param name="password">Пароль, переданный на сервер в HTTP-запросе.</param>
        /// <returns>Результат аутентификации.</returns>
        public bool IsAuthenticated(string password)
        {
            return Account.Authenticate(password?.ToCharArray() ?? Array.Empty<char>());
        }

        #endregion
    }
}
