using System;

namespace DevFactoryZ.CharityCRM.UI.Web.Configuration
{
    /// <summary>
    /// Описывает параметры конфигурации пользовательской сессии <see cref="Microsoft.AspNetCore.Http.ISession"/>.
    /// </summary>
    public interface ISessionConfig
    {
        /// <summary>
        /// Интервал неактивности пользователя (отстутствие входящих HTTP-запросов), 
        /// по истечении которого текущая сессия пользователя <see cref="Microsoft.AspNetCore.Http.ISession"/> закрывается на сервере.
        /// </summary>
        TimeSpan IdleTimeout { get; set; }
    }
}
