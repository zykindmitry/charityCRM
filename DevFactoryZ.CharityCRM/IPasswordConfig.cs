using System;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Конфигурация параметров сложности пароля.
    /// Используется в типе <see cref="Password"/> для валидации введенного пользователем нового проля, 
    /// при генерации случайного пароля, соответствующего заданной конфигурации,
    /// при хешировании введенного пользователем пароля.
    /// </summary>
    public interface IPasswordConfig : IAmPersistent<int>
    {
        /// <summary>
        /// Срок действия пароля.
        /// </summary>
        TimeSpan MaxLifeTime { get; }
        
        /// <summary>
        /// Минимальная длина пароля.
        /// </summary>
        int MinLength { get; }
        
        /// <summary>
        /// Длина "соли" (синхропосылки). 
        /// </summary>
        int SaltLength { get; }
        
        /// <summary>
        /// Локальный параметр.
        /// </summary>
        byte[] LocalSalt { get; }

        /// <summary>
        /// Массив дополнительных символов, которые могут использоваться для усложнения пароля.
        /// Применяется, когда флаг <see cref="UseSpecialSymbols"/> = true.
        /// </summary>
        char[] SpecialSymbols { get; }

        /// <summary>
        /// Флаг обязательного использования цифр для усложнения пароля.
        /// </summary>
        bool UseDigits { get; }

        /// <summary>
        /// Флаг обязательного использования дополнительных (специальных) символов для усложнения пароля.
        /// Набор дополнительных символов содержится в свойстве <see cref="SpecialSymbols"/>.
        /// </summary>
        bool UseSpecialSymbols { get; }

        /// <summary>
        /// Флаг обязательного использования букв в верхнем регистре для усложнения пароля.
        /// </summary>
        bool UseUpperCase { get; }

        /// <summary>
        /// Дата/время в формате UTC создания конфигурации сложности пароля.
        /// </summary>
        DateTime CreatedAt { get; }
    }
}
