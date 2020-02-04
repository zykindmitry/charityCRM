using System;

namespace DevFactoryZ.CharityCRM.UI.Web.Configuration
{
    /// <summary>
    /// Описывает параметры конфигурации пользовательской сессии <see cref="Microsoft.AspNetCore.Http.ISession"/>.
    /// </summary>
    public interface ISessionConfig
    {
        /// <summary>
        /// Интервал неактивности пользователя (отстутствие входящих HTTP-запросов) для сервеной сессии <see cref="Microsoft.AspNetCore.Http.ISession"/>, 
        /// по истечении которого серверная сессия текущего пользователя <see cref="Microsoft.AspNetCore.Http.ISession"/> закрывается.
        /// </summary>
        TimeSpan ServerSessionIdleTimeout { get; set; }

        /// <summary>
        /// Интервал неактивности пользователя (отстутствие входящих HTTP-запросов) для пользовательской сессии <see cref="AccountSession"/>,
        /// откпываемой приложеним.
        /// По истечении этого интервала текущая пользовательская сессия <see cref="AccountSession"/> закрывается приложением.
        /// Этот интервал должен быть не меньше, чем <see cref="ServerSessionIdleTimeout"/>.
        /// </summary>
        TimeSpan AccountSessionIdleTimeout { get; set; }

        /// <summary>
        /// Срок жизни текущей пользовательской сессии <see cref="AccountSession"/>. 
        /// По окончании этого срока текущая пользовательская сессия <see cref="AccountSession"/> закрывается приложением.
        /// </summary>
        TimeSpan AccountSessionExpiration { get; set; }

    }
}
