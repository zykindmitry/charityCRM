using System;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Описывает параметры конфигурации пользовательской сессии <see cref="AccountSession"/>, создаваемой приложением.
    /// </summary>
    public interface IAccountSessionConfig
    {
        /// <summary>
        /// Интервал неактивности пользователя (отстутствие входящих HTTP-запросов) для пользовательской сессии <see cref="AccountSession"/>,
        /// создаваемой приложеним.
        /// По истечении этого интервала текущая пользовательская сессия <see cref="AccountSession"/> закрывается приложением.
        /// </summary>
        TimeSpan AccountSessionIdleTimeout { get; set; }

        /// <summary>
        /// Срок жизни текущей пользовательской сессии <see cref="AccountSession"/>, создаваемой приложением. 
        /// По окончании этого срока текущая пользовательская сессия <see cref="AccountSession"/> закрывается приложением.
        /// </summary>
        TimeSpan AccountSessionExpiration { get; set; }

    }
}
