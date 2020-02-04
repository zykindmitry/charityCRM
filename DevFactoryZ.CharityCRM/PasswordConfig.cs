using System;
using System.Security.Cryptography;

namespace DevFactoryZ.CharityCRM
{   
    /// <summary>
    /// Имплементация <see cref="IPasswordConfig"/>. Содержит конфигурацию сложности пароля.
    /// </summary>
    public class PasswordConfig : IPasswordConfig
    {
        #region .ctor

        /// <summary>
        /// Создает экземпляр <see cref="PasswordConfig"/> со значениями параметров конфигурации по умолчанию.
        /// </summary>
        public PasswordConfig()
        {
            LocalSalt = GenerateRandom(SaltLength);
        }

        /// <summary>
        /// Создает экземпляр <see cref="PasswordConfig"/> с заданными значениями параметров конфигурации.
        /// </summary>
        /// <param name="maxLifeTime">Срок действия пароля.</param>
        /// <param name="minLength">Минимальная длина пароля.</param>
        /// <param name="saltLength">длина "соли" (синхропосылки).</param>
        /// <param name="useDigits">Флаг использования цифр для усложнения пароля.</param>
        /// <param name="useUpperCase">Флаг использования букв в верхнем регистре для усложнения пароля.</param>
        /// <param name="useSpecialSymbols">Флаг использования дополнительных (специальных) символов для усложнения пароля.</param>
        /// <param name="specialSymbols">Массив дополнительных (специальных) символов, которые могут использоваться для усложнения пароля.</param>
        public PasswordConfig(
            TimeSpan maxLifeTime
            , int minLength
            , int saltLength
            , bool useDigits
            , bool useUpperCase
            , bool useSpecialSymbols
            , char[] specialSymbols)
            : this()
        {
            MaxLifeTime = maxLifeTime;
            MinLength = minLength;
            SaltLength = saltLength;
            SpecialSymbols = specialSymbols;
            UseDigits = useDigits;
            UseSpecialSymbols = useSpecialSymbols;
            UseUpperCase = useUpperCase;
        }
        #endregion

        /// <summary>
        /// Возвращает уникальный идентификатор конфигурации сложности пароля.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// Возвращает признак допустимости удаления конфигурации сложности пароля.
        /// </summary>
        public bool CanBeDeleted => false;

        public TimeSpan MaxLifeTime { get; } = TimeSpan.FromDays(90);

        public int MinLength { get; } = 8;

        public int SaltLength { get; } = 4;

        public byte[] LocalSalt { get; }

        public char[] SpecialSymbols { get; protected set; } = "!#@$%&()[]{}_=".ToCharArray();

        public bool UseDigits { get; } = true;

        public bool UseSpecialSymbols { get; } = true;

        public bool UseUpperCase { get; } = true;

        public DateTime CreatedAt { get; } = DateTime.UtcNow;

        byte[] GenerateRandom(int size)
        {
            var result = new byte[size];
            using (var crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(result);
            }

            return result;
        }
    }
}
