using System;

namespace DevFactoryZ.CharityCRM.UI.Web.Configuration
{
    /// <summary>
    /// Имплементация <see cref="ICookieConfig"/>.
    /// Инициализируется в конструкторе <see cref="Startup"/> путем загрузки из конфигурационного файла 'security_settings.json'.
    /// </summary>
    internal class CookieConfig : ICookieConfig
    {
        public string Name { get; set; }

        public string Domain { get; set; }

        public bool HttpOnly { get; set; }

        public string Path { get; set; }

        public TimeSpan? Expiration { get; set; }

        public TimeSpan? MaxAge { get; set; }

        public bool IsEssential { get; set; }
    }
}
