using System;

namespace DevFactoryZ.CharityCRM.UI.Web.Configuration
{
    /// <summary>
    /// Описывает параметры конфигурации серверной пользовательской сессии <see cref="Microsoft.AspNetCore.Http.ISession"/>.
    /// </summary>
    public interface IServerSessionConfig
    {
        /// <summary>
        /// Интервал неактивности пользователя (отстутствие входящих HTTP-запросов) для сервеной сессии <see cref="Microsoft.AspNetCore.Http.ISession"/>, 
        /// по истечении которого серверная сессия текущего пользователя <see cref="Microsoft.AspNetCore.Http.ISession"/> закрывается сервером.
        /// </summary>
        TimeSpan ServerSessionIdleTimeout { get; set; }
    }
}
