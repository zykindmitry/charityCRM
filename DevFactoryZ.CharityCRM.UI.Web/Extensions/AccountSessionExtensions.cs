using System;
using Microsoft.AspNetCore.Http;
using DevFactoryZ.CharityCRM.Persistence;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Содержит методы расширений для работы с <see cref="AccountSession"/>.
    /// </summary>
    public static class AccountSessionExtensions
    {
        /// <summary>
        /// Проверяет подлинность HTTP-запроса путем сравнения IP-адреса клиента и User-Agent из HTTP-запроса со значениями, 
        /// хранящимися в <see cref="AccountSession"/> на сервере.
        /// <para>При любом несовпадении генерирует <see cref="ValidationException"/>.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ValidationException"></exception>
        /// <param name="accountSession"><see cref="AccountSession"/> на сервере, содержащая правильные значения IP-адреса клиента и User-Agent.</param>
        /// <param name="context">Текущий <see cref="HttpContext"/>.</param>
        /// <returns>Результат проверки.</returns>
        public static bool IsNoFake(this AccountSession accountSession
            , HttpContext context)
        {
            var userAgent = context?.GetUserAgent()
                ?? throw new ArgumentNullException(nameof(context), $"{nameof(HttpContext)} is null."); 

            var ipAddress = context.GetIpAddress();

            return accountSession.UserAgent.Equals(userAgent, StringComparison.InvariantCulture)
                && accountSession.IPAddress.Equals(ipAddress, StringComparison.InvariantCulture)
                ? true
                : throw new ValidationException("'User-Agent' или IP-адрес запроса не совпадает с 'User-Agent' или IP-адресом пользовательской сессии.");
        }

        /// <summary>
        /// Проверяет актуальность пользовательской сессии путем сравнения времени окончания срока жизни, хранящегося в текущем <see cref="AccountSession"/>, 
        /// с текущим временем на сервере.
        /// </summary>
        /// <param name="accountSession">Текущая <see cref="AccountSession"/> на сервере.</param>
        /// <returns>Результат проверки.</returns>
        public static bool IsNoExpired(this AccountSession accountSession)
        {
            return accountSession.ExpiredAt >= DateTime.UtcNow;
        }

        /// <summary>
        /// Аутентификация пользователя по паролю с использованием метода <see cref="Account.Authenticate(char[])"/>.
        /// </summary>
        /// <exception cref="ValidationException"></exception>
        /// <param name="accountSession">Текущая <see cref="AccountSession"/> на сервере.</param>
        /// <param name="password">Пароль, переданный на сервер в HTTP-запросе.</param>
        /// <returns>Результат аутентификации.</returns>
        public static bool IsAuthenticated(this AccountSession accountSession
            , string password)
        {

            return accountSession.Account.Authenticate(password?.ToCharArray())
                ? true
                : throw new ValidationException("Неверное имя пользоателя или пароль.");           
        }

        /// <summary>
        /// Сохранение изменений текущей пользовательской сессии в хранилище данных.
        /// </summary>
        /// <param name="accountSession">Текущая <see cref="AccountSession"/> на сервере.</param>
        /// <param name="repositoryCreatorFactory">Фабрика создателей репозиториев <see cref="IRepositoryCreatorFactory"/>.</param>
        /// <returns>Текущая <see cref="AccountSession"/> на сервере.</returns>
        public static AccountSession UpdateRepository(this AccountSession accountSession
            , IRepositoryCreatorFactory repositoryCreatorFactory)
        {
            var accountSessionRepository = repositoryCreatorFactory?
                .GetRepositoryCreator<IAccountSessionRepository>()?
                .Create() 
                ?? throw new ArgumentException(
                    $"Фабрика создателей репозиториев '{nameof(repositoryCreatorFactory)}' не инициализирована или не содержит '{nameof(IAccountSessionRepository)}'."
                    , nameof(repositoryCreatorFactory));

            accountSession = accountSessionRepository.Update(accountSession);
            accountSessionRepository.Save();

            return accountSession;
        }
    }    
}
