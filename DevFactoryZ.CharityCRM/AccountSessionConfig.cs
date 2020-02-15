using System;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Имплементация <see cref="IAccountSessionConfig"/>.
    /// Инициализируется в конструкторе <see cref="DevFactoryZ.CharityCRM.Ui.Web.Startup"/> путем загрузки из конфигурационного файла 'security_settings.json'.
    /// </summary>
    public class AccountSessionConfig : IAccountSessionConfig
    {
        public TimeSpan AccountSessionIdleTimeout { get; set; }
        
        public TimeSpan AccountSessionExpiration { get; set; }
    }
}
