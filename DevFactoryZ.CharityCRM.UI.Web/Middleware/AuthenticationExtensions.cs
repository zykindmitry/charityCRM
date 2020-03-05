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
        /// Для работы с хранилищем использует <see cref="IAccountSessionService"/>.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="cookieConfig">Параметры конфигурации Cookies.</param>
        /// <returns></returns>
        internal static IApplicationBuilder UseCharityAuthentication(
            this IApplicationBuilder builder
            , ICookieConfig cookieConfig)
        {
            return builder.UseMiddleware<CharityAuthentication>(cookieConfig);
        }
    }
}
