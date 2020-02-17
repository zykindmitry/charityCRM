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
        /// <param name="cookieConfig">Параметры конфигурации Cookies.</param>
        /// <returns></returns>
        internal static IApplicationBuilder UseCharityAuthentication(
            this IApplicationBuilder builder
            , IRepositoryCreatorFactory repositoryCreatorFactory
            , ICookieConfig cookieConfig)
        {
            return builder.UseMiddleware<CharityAuthentication>(repositoryCreatorFactory, cookieConfig);
        }

        /// <summary>
        /// Метод расширения для добавления <see cref="CharityAuthentication"/> в конвейер обработки входящих HTTP-запросов.
        /// Для работы с хранилищем использует <see cref="IAccountService"/>, <see cref="IAccountSessionService"/>.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="accountSessionService">Сервис работы с хранилищем для <see cref="AccountSession"/>.</param>
        /// <param name="cookieConfig">Параметры конфигурации Cookies.</param>
        /// <returns></returns>
        internal static IApplicationBuilder UseCharityAuthentication(
            this IApplicationBuilder builder
            , IAccountSessionService accountSessionService
            , ICookieConfig cookieConfig)
        {
            return builder.UseMiddleware<CharityAuthentication>(accountSessionService, cookieConfig);
        }
    }
}
