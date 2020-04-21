using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// <para>Представляет пароль пользователя.</para>
    /// Содержит методы: для валидации (<see cref="Validate(char[], IPasswordConfig, out List{string})"/>), 
    /// хешироавния введенного пароля (<see cref="HashPassword(char[])"/>), 
    /// генерации случайного пароля (<see cref="Generate"/>), сравнения паролей (<see cref="Equals(object)"/>),
    /// сброса пароля (<see cref="Reset"/>).
    /// </summary>
    public class Password
    {
        /// <summary>
        /// Конфигурация параметров сложности пароля.
        /// </summary>
        /// <value><see cref="IPasswordConfig"/></value>
        public PasswordConfig PasswordConfig { get; private set; }

        #region .ctor

        /// <summary>
        /// Для создания миграции.
        /// </summary>
        protected Password()
        {
        }

        /// <summary>
        ///  ИСПОЛЬЗУЕТСЯ ДЛЯ РЕГИСТРАЦИИ НОВОГО ПОЛЬЗОВАТЕЛЯ. Создает экземпляр <see cref="Password"/> со сгенерированным случайным временным паролем.
        /// </summary>
        /// <param name="config">Конфигурация параметров сложности пароля.</param>
        public Password(IPasswordConfig config)
           : this(config
                  , null
                  , Array.Empty<byte>())
        {
            Reset();
        }

        /// <summary>
        /// Используется при первом входе пользоателя в систему или смене пароля.
        /// <para><c>new Password(config, passwordText)</c> - cоздает экземпляр <see cref="Password"/> с паролем, введенным пользователем, и новой "солью". </para>
        /// </summary>
        /// <param name="config">Конфигурация параметров сложности пароля.</param>
        /// <param name="clearText">Текст пароля.</param>
        public Password(IPasswordConfig config, char[] clearText)
           : this(config
                  , clearText ?? Array.Empty<char>()
                  , Array.Empty<byte>())
        {
            ChangedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Используется при сравнении паролей с одинаковой "солью".
        /// <para><c>new Password(config, passwordText, saltText)</c> - cоздает экземпляр <see cref="Password"/> с паролем, введенным пользователем, и указанной "солью".</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <param name="config">Конфигурация параметров сложности пароля.</param>
        /// <param name="clearText">Текст пароля.</param>
        /// <param name="salt">Соль" (синхропосылка).</param>
        public Password(IPasswordConfig config, char[] clearText, byte[] salt)
            : this()
        {
            PasswordConfig = (PasswordConfig)config
                ?? throw new ArgumentNullException(
                    nameof(config),
                    "Не инициализирована конфигурация параметров сложности пароля.");
            
            if (clearText != null && !Password.Validate(clearText, PasswordConfig, out List<string> errortext))
            {
                throw new ArgumentException(string.Join(" ", errortext), nameof(clearText));
            }

            TemporaryPassword = null;
            RawSalt = salt.Length == 0 ? GenerateRandom(PasswordConfig.SaltLength) : salt;
            RawHash = HashPassword(clearText ?? Array.Empty<char>());
        }

        /// <summary>
        /// Создает экземпляр <see cref="Password"/> с заданными значениями компонентов пароля: "соль", хеш пароля, дата последнего измнения.
        /// Используется для получения данных из хранилища. 
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <param name="config">Конфигурация параметров сложности пароля.</param>
        /// <param name="salt">"Соль" (синхропосылка).</param>
        /// <param name="hash">Хеш пароля.</param>
        /// <param name="changedAt">Дата последнего изменения пароля.</param>
        public Password(IPasswordConfig config, byte[] salt, byte[] hash, DateTime? changedAt)
            : this(config
                  , null
                  , Array.Empty<byte>())
        {
            RawSalt = salt;
            RawHash = hash;
            ChangedAt = changedAt;
        }

        #endregion

        #region Хеш пароля, соль

        /// <summary>
        /// "Соль" (синхропосылка) в формате Base64String.
        /// </summary>
        public string Salt
        {
            get { return RawSalt != null ? Convert.ToBase64String(RawSalt) : string.Empty; }
        }

        /// <summary>
        /// "Соль" (синхропосылка).
        /// </summary>
        public byte[] RawSalt { get; private set; }

        /// <summary>
        /// Хеш пароля в формате Base64String.
        /// </summary>
        public string Hash
        {
            get { return RawHash != null ? Convert.ToBase64String(RawHash) : string.Empty; }
        }

        /// <summary>
        /// Хеш пароля.
        /// </summary>
        public byte[] RawHash { get; private set; }

        #endregion

        #region Проверка срока жизни пароля

        /// <summary>
        /// Дата последнего изменения пароля в формате UTC.
        /// Используется для определения первого входа в систему для нового пользователя ( когда <see cref="ChangedAt"/> = null),
        /// а также для определения окончания срока действия пароля.
        /// </summary>
        public DateTime? ChangedAt { get; private set; }

        /// <summary>
        /// Проверка срока действия пароля. 
        /// Если поле <see cref="ChangedAt"/> = null, значит, пароль ни разу не изменялся.
        /// В этом случае, а также в случае, если после смены пароля прошло больше времени, чем указано в свойстве <see cref="IPasswordConfig.MaxLifeTime"/> конфигурации сложности пароля,
        /// считаем, что пароль просрочен.
        /// </summary>
        public bool IsAlive
        {
            get
            {
                return ChangedAt.HasValue   
                    && DateTime.UtcNow.Subtract((DateTime)ChangedAt) <= PasswordConfig?.MaxLifeTime;
            }
        }

        #endregion

        #region Валидация текстового представления пароля на соответствование требованиям сложности

        /// <summary>
        /// <para>Минимальная длина пароля, при которой возможно выполнить все требования по сложности: </para>
        /// <see cref="IPasswordConfig.UseDigits"/>
        /// <para><see cref="IPasswordConfig.UseUpperCase"/></para>
        /// <see cref="IPasswordConfig.UseSpecialSymbols"/>
        /// </summary>
        const int MinPasswordLength = 3;

        /// <summary>
        /// Проверка сложности пароля на соответствие заданным параметрам:
        /// наличие цифр, букв в верхнем регистре, специальных символов (при включенных соответствующих флагах в конфигурации сложности),
        /// а также минимальной длине.
        /// </summary>
        /// <param name="clearText">Проверяемый пароль.</param>
        /// <param name="config">Конфигурация параметров сложности пароля.</param>
        /// <param name="errorText">Возвращаемый список ошибок проверки.</param>
        /// <returns>Результат проверки: true, если ошибок нет, false - если найдены ошибки.</returns>
        public static bool Validate(char[] clearText, IPasswordConfig config, out List<string> errorText)
        {
            errorText = new List<string>();

            if (config.MinLength > clearText.Length)
            {
                errorText.Add($"Длина пароля должна быть не менее {config.MinLength} символов.");
            }

            if (config.UseDigits && (clearText.Where(s => char.IsDigit(s)).Count() == 0))
            {
                errorText.Add("Пароль должен содержать хотя бы одну цифру.");
            }

            if (config.UseUpperCase && (clearText.Where(s => char.IsUpper(s)).Count() == 0))
            {
                errorText.Add("Пароль должен содержать хотя бы один символ в верхнем регистре.");
            }

            if (config.UseSpecialSymbols && !clearText.Any(s => config.SpecialSymbols.Contains(s)))
            {
                errorText.Add($"Пароль должен содержать хотя бы один из символов {string.Join(",", config.SpecialSymbols)}.");
            }

            return !errorText.Any();
        }

        #endregion

        #region Генерация случайного пароля, соли

        /// <summary>
        /// Временный случайный пароль, автоматически сгенерированный в методе <see cref="Generate"/>.
        /// </summary>
        public char[] TemporaryPassword { get; private set; }

        /// <summary>
        /// Генерация случайного пароля в соответствии с заданной конфигурацией параметров сложности пароля.
        /// </summary>
        /// <returns>Сгенерированный случайный пароль.</returns>
        char[] Generate()
        {
            if (PasswordConfig.MinLength == 0)
            {
                return Array.Empty<char>();
            }

            var lowers = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            var uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            var digits = "1234567890".ToCharArray();

            var allChars = lowers.Concat(uppers).Concat(digits).Concat(PasswordConfig.SpecialSymbols).ToArray();

            var data = GenerateRandom(PasswordConfig.MinLength);

            var result = new StringBuilder(PasswordConfig.MinLength);

            foreach (byte b in data)
            {
                result.Append(allChars[b % (allChars.Length)]);
            }

            if (result.Length >= MinPasswordLength)
            {
                if (PasswordConfig.UseDigits && result.ToString().Where(s => char.IsDigit(s)).Count() == 0)
                {
                    result.Remove(result.Length - 1, 1);
                    result.Append(digits[data[0] % digits.Length]);
                }

                if (PasswordConfig.UseUpperCase && result.ToString().Where(s => char.IsUpper(s)).Count() == 0)
                {
                    result.Remove(result.Length - 2, 1);
                    result.Insert(result.Length - 1, uppers[data[1] % uppers.Length]);
                }

                if (PasswordConfig.UseSpecialSymbols && !result.ToString().Any(s => PasswordConfig.SpecialSymbols.Contains(s)))
                {
                    result.Remove(result.Length - 3, 1);
                    result.Insert(result.Length - 2, PasswordConfig.SpecialSymbols[data[2] % PasswordConfig.SpecialSymbols.Length]);
                }
            }

            return result.ToString().ToCharArray();
        }

        /// <summary>
        /// Генерация случайного массива байт заданного размера.
        /// </summary>
        /// <param name="size">Размер массива.</param>
        /// <returns>Сгенерированный массив.</returns>
        static byte[] GenerateRandom(int size)
        {
            var result = new byte[size];
            using (var crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(result);
            }

            return result;
        }

        #endregion

        #region Генерация хеша, изменение, сброс пароля

        /// <summary>
        /// Создание хеша пароля, с использованием "соли" (синхропосылки) и локального параметра.
        /// </summary>
        /// <param name="clearText">Пароль для хеширования.</param>
        /// <returns>Хеш пароля.</returns>
        byte[] HashPassword(char[] clearText)
        {
            var utf8 = Encoding.UTF8;
            byte[] hash;

            // создаем рабочий массив достаточного размера, чтобы вместить пароль, соль и локальный параметр
            var data = new byte[utf8.GetMaxByteCount(clearText.Length)
                + RawSalt.Length
                + PasswordConfig.LocalSalt.Length];

            try
            {
                // копируем пароль в рабочий массив, преобразуя его в UTF-8
                var byteCount = utf8.GetBytes(clearText, 0, clearText.Length, data, 0);

                // копируем синхропосылку в рабочий массив
                Array.Copy(RawSalt, 0, data, byteCount, RawSalt.Length);

                // копируем локальный параматр в рабочий массив
                Array.Copy(PasswordConfig.LocalSalt, 0, data, byteCount + RawSalt.Length, PasswordConfig.LocalSalt.Length);

                // хэшируем данные массива
                using (var hashAlgorithm = new SHA512Managed())
                {
                    hash = hashAlgorithm.ComputeHash(data, 0, RawSalt.Length + byteCount);
                }
            }
            finally
            {
                // очищаем рабочий массив в конце работы, чтобы избежать утечки открытого пароля
                Array.Clear(data, 0, data.Length);
            }

            return hash;
        }

        /// <summary>
        /// Изменение пароля. При изменении используется актуальная конфигурация сложности пароля.
        /// При равенстве нового и старого паролей поле <see cref="ChangedAt"/> не обновляется.
        /// </summary>
        /// <param name="newPasswordClearText">Текстовое представление нового пароля.</param>
        /// <param name="actualPasswordConfig">Актуальная конфигурация сложности пароля для создания нового пароля.</param>
        public void ChangeTo(char[] newPasswordClearText, IPasswordConfig actualPasswordConfig)
        {
            if (actualPasswordConfig == null)
            {
                throw new ArgumentNullException(nameof(actualPasswordConfig));
            }

            // Проверяем новый пароль на соотвествие актуальной конфигурации сложности
            if (!Validate(newPasswordClearText ?? Array.Empty<char>(), actualPasswordConfig, out List<string> errors))
            {
                throw new ArgumentException(string.Join(" ", errors), nameof(newPasswordClearText));
            }

            // Хеш нового пароля со старой "солью".
            var newRawHash = HashPassword(newPasswordClearText);
            
            // Если хеш нового пароля не равен старому хешу, то новый пароль отличается от старого. Создаем новый хеш.
            if (!Equals(newRawHash))
            {
                PasswordConfig = (PasswordConfig)actualPasswordConfig;
                RawSalt = GenerateRandom(PasswordConfig.SaltLength);
                RawHash = HashPassword(newPasswordClearText);
                ChangedAt = DateTime.UtcNow;
            }
        }

        private void Reset()
        {
            ChangedAt = null;
            TemporaryPassword = Generate();
            RawSalt = GenerateRandom(PasswordConfig.SaltLength);
            RawHash = HashPassword(TemporaryPassword);
        }

        /// <summary>
        /// Сброс пароля. При сбросе используется актуальная конфигурация сложности пароля.
        /// Генерирует случайный временный пароль c новой "солью", сбрасывает дату последнего изменения пароля.
        /// </summary>
        public void Reset(IPasswordConfig actualPasswordConfig)
        {
            PasswordConfig = (PasswordConfig)actualPasswordConfig 
                ?? throw new ArgumentNullException(nameof(actualPasswordConfig));
            
            Reset();
        }

        #endregion

        #region Переопределенные методы

        /// <summary>
        /// Сравнение текущего экземпляра пароля с другим экземпляром пароля.
        /// Если passwordToCompare успешно приведен к <see cref="Password"/>, 
        /// сравниваются побайтно поле <see cref="RawHash"/> текущего экземпляра и поле <see cref="RawHash"/> результата приведения.
        /// Если <see cref="passwordToCompare"/> успешно приведен к <see cref="byte[]"/>, сравниваются побайтно поле <see cref="RawHash"/> текущего экземпляра и результат приведения.
        /// Родительский Equals не используется.
        /// </summary>
        /// <param name="passwordToCompare">Экземпляр пароля, с которым сравнивается текущий экземпляр.</param>
        /// <returns>Результат проверки: true, если поля <see cref="RawHash"/> равны, false - в ином случае.</returns>
        public override bool Equals(object passwordToCompare)
        {
            var password = passwordToCompare as Password;

            var hashToCompare = password != null
                ? password.RawHash
                : passwordToCompare as byte[] ?? Array.Empty<byte>();

            return Enumerable.SequenceEqual(RawHash, hashToCompare);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
