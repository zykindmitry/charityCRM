using System;

namespace DevFactoryZ.CharityCRM.UI.Web.Configuration
{
    /// <summary>
    /// Описывает параметры конфигурации Cookies.
    /// </summary>
    public interface ICookieConfig
    {
        /// <summary>
        /// Имя куки.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Домен, для которого устаналиваются куки.
        /// </summary>
        string Domain { get; set; }

        /// <summary>
        /// Доступны ли куки только при передаче через HTTP-запрос.
        /// </summary>
        bool HttpOnly { get; set; }

        /// <summary>
        /// Путь, который используется куками.
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// Срок жизни куки.
        /// </summary>
        TimeSpan? Expiration { get; set; }

        /// <summary>
        /// Максимальный возраст куки.
        /// </summary>
        TimeSpan? MaxAge { get; set; }

        /// <summary>
        /// При значении 'true' указывает, что куки критичны и необходимы для работы этого приложения.
        /// </summary>
        bool IsEssential { get; set; }
    }
}
