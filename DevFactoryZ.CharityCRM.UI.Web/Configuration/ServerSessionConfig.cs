using System;

namespace DevFactoryZ.CharityCRM.UI.Web.Configuration
{
    /// <summary>
    /// Имплементация <see cref="IServerSessionConfig"/>.
    /// Инициализируется в конструкторе <see cref="Startup"/> путем загрузки из конфигурационного файла 'security_settings.json'.
    /// </summary>
    internal class ServerSessionConfig : IServerSessionConfig
    {
        public TimeSpan ServerSessionIdleTimeout { get; set; }
    }
}
