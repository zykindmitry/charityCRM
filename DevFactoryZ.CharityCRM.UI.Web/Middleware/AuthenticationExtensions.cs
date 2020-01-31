using Microsoft.AspNetCore.Builder;
using DevFactoryZ.CharityCRM.Persistence;
using DevFactoryZ.CharityCRM.Services;
using DevFactoryZ.CharityCRM.UI.Web.Configuration;

namespace DevFactoryZ.CharityCRM.UI.Web.Middleware
{
    /// <summary>
    /// Содержит методы расширений для работы с <see cref="CharityAuthentication"/>
    /// </summary>
    public static class AuthenticationExtensions
    {
        /// <summary>
        /// Метод расширения для добавления <see cref="CharityAuthentication"/> в конвейер обработки входящих HTTP-запросов.
        /// Для работы с хранилищем использует <see cref="IRepositoryCreatorFactory"/>.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="repositoryCreatorFactory">Фабрика создателей репозиториев <see cref="IRepositoryCreatorFactory"/>.</param>
        /// <param name="sessionConfig">Параметры конфигурации объекта <see cref="Microsoft.AspNetCore.Http.ISession"/>.</param>
        /// <param name="cookieConfig">Параметры конфигурации Cookies.</param>
        /// <returns></returns>
        internal static IApplicationBuilder UseCharityAuthentication(this IApplicationBuilder builder
            , IRepositoryCreatorFactory repositoryCreatorFactory
            , ISessionConfig sessionConfig
            , ICookieConfig cookieConfig)
        {
            return builder.UseMiddleware<CharityAuthentication>(repositoryCreatorFactory, sessionConfig, cookieConfig);
        }

        /// <summary>
        /// Метод расширения для добавления <see cref="CharityAuthentication"/> в конвейер обработки входящих HTTP-запросов.
        /// Для работы с хранилищем использует <see cref="IAccountService"/>, <see cref="IAccountSessionService"/>.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="accountService">Сервис работы с хранилищем для <see cref="Account"/>.</param>
        /// <param name="accountSessionService">Сервис работы с хранилищем для <see cref="AccountSession"/>.</param>
        /// <param name="sessionConfig">Параметры конфигурации объекта <see cref="Microsoft.AspNetCore.Http.ISession"/>.</param>
        /// <param name="cookieConfig">Параметры конфигурации Cookies.</param>
        /// <returns></returns>
        internal static IApplicationBuilder UseCharityAuthentication(this IApplicationBuilder builder
            , IAccountService accountService
            , IAccountSessionService accountSessionService
            , ISessionConfig sessionConfig
            , ICookieConfig cookieConfig)
        {
            return builder.UseMiddleware<CharityAuthentication>(accountService, accountSessionService, sessionConfig, cookieConfig);
        }
    }
}
