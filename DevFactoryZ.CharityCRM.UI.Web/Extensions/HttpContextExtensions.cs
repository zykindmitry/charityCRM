using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace DevFactoryZ.CharityCRM.UI.Web
{
    /// <summary>
    /// Содержит методы расширений для работы с <see cref="HttpContext"/>.
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Метод расширения для извлечения содержимого User-Agent текущего <see cref="HttpContext"/>.
        /// <para>При отсутствии содержимого User-Agent возвращает пустую строку.</para>
        /// </summary>
        /// <param name="context"><see cref="HttpContext"/>, из которого извлекается User-Agent.</param>
        /// <returns>Строка с содержимым User-Agent текущего <see cref="HttpContext"/>.</returns>
        public static string GetUserAgent(this HttpContext context)
        {
            return context.Request.Headers.TryGetValue("User-Agent", out StringValues values)
                ? values.Count > 0
                    ? string.Join(" : ", values.ToArray())
                    : string.Empty
                : string.Empty;
        }

        /// <summary>
        /// Метод расширения для извлечения IP-адреса клиента из текущего <see cref="HttpContext"/>.
        /// <para>При отсутствии IP-адреса клиента возвращает пустую строку.</para>
        /// </summary>
        /// <param name="context"><see cref="HttpContext"/>, из которого извлекается IP-адрес клиента.</param>
        /// <returns>Строка с IP-адресом клиента из текущего <see cref="HttpContext"/>.</returns>
        public static string GetIpAddress(this HttpContext context)
        {
            return context.Request.HttpContext.Connection?.RemoteIpAddress?.ToString()
                ?? string.Empty;
        }
    }    
}
