using System;

namespace DevFactoryZ.CharityCRM.UI.Web.Configuration
{
    /// <summary>
    /// Имплементация <see cref="ISessionConfig"/>.
    /// Инициализируется в конструкторе <see cref="Startup"/> путем загрузки из конфигурационного файла 'security_settings.json'.
    /// </summary>
    internal class SessionConfig : ISessionConfig
    {
        public TimeSpan IdleTimeout { get; set; }
    }
}
