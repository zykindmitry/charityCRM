using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Содержит методы расширений для работы с <see cref="HttpContext"/>.
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Метод расширения для извлечения содержимого User-Agent текущего <see cref="HttpContext"/>.
        /// <para>При отсутствии или некорректности User-Agent генерирует <see cref="ValidationException"/></para>
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ValidationException"></exception>
        /// <param name="context"><see cref="HttpContext"/>, из которого извлекается User-Agent.</param>
        /// <returns>Строка с содержимым User-Agent текущего <see cref="HttpContext"/>.</returns>
        public static string GetUserAgent(this HttpContext context)
        {
            if (context.Request == null)
            {
                throw new ArgumentException($"Некорректный запрос: {nameof(context.Request)} is null.", nameof(context.Request));
            }
            else if (context.Request.Headers == null)
            {
                throw new ArgumentException($"Некорректный запрос: {nameof(context.Request.Headers)} is null.", nameof(context.Request.Headers));
            }

            return context.Request.Headers.TryGetValue("User-Agent", out StringValues values) 
                ? values.Count > 0
                    ? values.ToString()
                    : throw new ValidationException("Заголовок 'User-Agent' в запросе пустой.") 
                : throw new ValidationException("Заголовок 'User-Agent' в запросе отсутствует .");
        }

        /// <summary>
        /// Метод расширения для извлечения IP-адреса клиента из текущего <see cref="HttpContext"/>.
        /// <para>При отсутствии IP-адреса клиента или некорректности запроса генерирует <see cref="ArgumentException"/></para>
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        /// <param name="context"><see cref="HttpContext"/>, из которого извлекается IP-адрес клиента.</param>
        /// <returns>Строка с IP-адресом клиента из текущего <see cref="HttpContext"/>.</returns>
        public static string GetIpAddress(this HttpContext context)
        {
            if (context.Request == null)
            {
                throw new ArgumentException($"Некорректный запрос: {nameof(context.Request)} is null.", nameof(context.Request));
            }
            else if (context.Request.HttpContext == null)
            {
                throw new ArgumentException($"Некорректный запрос: {nameof(context.Request.HttpContext)} is null.", nameof(context.Request.HttpContext));
            }
            else if (context.Request.HttpContext.Connection == null)
            {
                throw new ArgumentException(
                    $"Некорректный запрос: {nameof(context.Request.HttpContext.Connection)} is null.", nameof(context.Request.HttpContext.Connection));
            }

            return context.Request.HttpContext.Connection.RemoteIpAddress?.ToString()
                ?? throw new ArgumentException(
                    $"Некорректный запрос: {nameof(context.Request.HttpContext.Connection.RemoteIpAddress)} is null."
                    , nameof(context.Request.HttpContext.Connection.RemoteIpAddress));
        }
    }    
}
