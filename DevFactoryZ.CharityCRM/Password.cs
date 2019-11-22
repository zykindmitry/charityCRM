using System;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Представляет пароль пользователя.
    /// Содержит методы для валидации, хешироавния введенного пароля, генерации случайного пароля, сравнения паролей.
    /// </summary>
    public class Password
    {
        /// <summary>
        /// Конфигурация параметров сложности пароля.
        /// </summary>
        public IPasswordConfig Config { get; }

        /// <summary>
        /// ВНИМАНИЕ!!! ИСПОЛЬЗУЕТСЯ ДЛЯ РЕГИСТРАЦИИ НОВОГО ПОЛЬЗОВАТЕЛЯ, ДЛЯ СБРОСА ПАРОЛЯ.
        /// Создает экземпляр Password со сгенерированным случайным временным паролем.
        /// Временный пароль хешируется, текст пароля помещается в Password.TemporaryPassword.
        /// </summary>
        /// <param name="config">Конфигурация параметров сложности пароля.</param>
        public Password(IPasswordConfig config)
        {
            Config = config 
                ?? throw new ArgumentNullException(nameof(config), "Не инициализирована конфигурация параметров сложности пароля.");

            ChangedAt = null;
            TemporaryPassword = Generate();
            RawSalt = GenerateRandom(Config.SaltLength);
            RawHash = HashPassword(TemporaryPassword);
        }

        /// <summary>
        /// Создает экземпляр Password с паролем, введенным пользователем. 
        /// Если введенный пароль не соответствует параметрам сложности, выбрасывает ArgumentException.
        /// При хешировании используется указанное в параметре salt значение "соли". Если параметр salt опущен, генерится новая "соль".
        /// </summary>
        /// <param name="clearText">Введенный пароль.</param>
        /// <param name="config">Конфигурация параметров сложности пароля.</param>
        /// <param name="salt">"Соль", или синхропосылка.</param>
        public Password(char[] clearText, IPasswordConfig config, string salt = "")
        {
            Config = config 
                ?? throw new ArgumentNullException(nameof(config), "Не инициализирована конфигурация параметров сложности пароля.");
            
            if (!Password.IsValid(clearText, Config, out List<string> errortext))
                throw new ArgumentException(string.Join(" ", errortext), nameof(clearText));

            RawSalt = string.IsNullOrWhiteSpace(salt) ? GenerateRandom(Config.SaltLength) : Convert.FromBase64String(salt);
            RawHash = HashPassword(clearText);
        }

        /// <summary>
        /// Создает экземпляр Password с заданными значениями компонентов пароля: "соль", хеш пароля.
        /// Может использоваться при получении данных из хранилища.
        /// </summary>
        /// <param name="salt">"Соль" (синхропосылка).</param>
        /// <param name="hash">Хеш пароля.</param>
        /// <param name="config">Конфигурация параметров сложности пароля.</param>
        public Password(string salt, string hash, DateTime? changedAt, IPasswordConfig config)
        {
            Config = config 
                ?? throw new ArgumentNullException(nameof(config), "Не инициализирована конфигурация параметров сложности пароля.");

            RawSalt = string.IsNullOrWhiteSpace(salt) ? Array.Empty<byte>() : Convert.FromBase64String(salt);
            RawHash = string.IsNullOrWhiteSpace(hash) ? Array.Empty<byte>() : Convert.FromBase64String(hash);
            ChangedAt = changedAt;
        }

        /// <summary>
        /// Создает экземпляр Password с заданными значениями компонентов пароля: "соль", хеш пароля.
        /// Может использоваться при получении данных из хранилища.
        /// </summary>
        /// <param name="salt">"Соль" (синхропосылка).</param>
        /// <param name="hash">Хеш пароля.</param>
        /// <param name="config">Конфигурация параметров сложности пароля.</param>
        public Password(byte[] salt, byte[] hash, DateTime? changedAt, IPasswordConfig config)
        {
            Config = config 
                ?? throw new ArgumentNullException(nameof(config), "Не инициализирована конфигурация параметров сложности пароля.");

            RawSalt = salt;
            RawHash = hash;
            ChangedAt = changedAt;
        }

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

        /// <summary>
        /// Дата последнего изменения пароля в формате UTC.
        /// Используется для определения первого входа в систему для нового пользователя ( когда ChangedAt = null),
        /// а также для определения окончания срока действия пароля.
        /// </summary>
        public DateTime? ChangedAt { get; private set; }

        /// <summary>
        /// Проверка срока действия пароля. 
        /// Если поле Password.ChangedAt = null, значит, пароль ни разу не изменялся.
        /// В этом случае, а также в случае, если после смены пароля прошло больше времени, чем указано в параметре MaxLifeTime конфигурации сложности пароля,
        /// считаем, что пароль просрочен.
        /// </summary>
        public bool IsAlive
        {
            get
            {
                return ChangedAt != null
                    && DateTime.UtcNow.Subtract((DateTime)ChangedAt) <= Config.MaxLifeTime;
            }
        }

        /// <summary>
        /// Проверка сложности пароля на соответствие заданным параметрам:
        /// наличие цифр, букв в верхнем регистре, специальных символов (при включенных соответствующих флагах в конфигурации сложности),
        /// а также минимальной длине.
        /// </summary>
        /// <param name="clearText">Проверяемый пароль.</param>
        /// <param name="errorText">Возвращаемый список ошибок проверки.</param>
        /// <returns>Результат проверки: true, если ошибок нет, false - если найдены ошибки.</returns>
        public static bool IsValid(char[] clearText, IPasswordConfig config, out List<string> errorText)
        {
            errorText = new List<string>();
            var returnResult = true;

            if (config.MinLength > clearText.Length)
            {
                errorText.Add($"Длина пароля должна быть не менее {config.MinLength} символов.");
                returnResult = false;
            }

            if (config.UseDigits && (clearText.Where(s => char.IsDigit(s)).Count() == 0))
            {
                errorText.Add("Пароль должен содержать хотя бы одну цифру.");
                returnResult = false;
            }

            if (config.UseUpperCase && (clearText.Where(s => char.IsUpper(s)).Count() == 0))
            {
                errorText.Add("Пароль должен содержать хотя бы один символ в верхнем регистре.");
                returnResult = false;
            }

            if (config.UseSpecialSymbols && !clearText.Any(s => config.SpecialSymbols.Contains(s)))
            {
                errorText.Add($"Пароль должен содержать хотя бы один из символов {string.Join(",", config.SpecialSymbols)}.");
                returnResult = false;
            }

            return returnResult;
        }

        /// <summary>
        /// Сравнение текущего экземпляра пароля с другим экземпляром пароля.
        /// Если password имеет тип Password или byte[], сравниваются побайтно поля RawHash.
        /// В ином случае вызывается родительский метод Equals.
        /// </summary>
        /// <param name="password">Экземпляр пароля, с которым сравнивается текущий экземпляр.</param>
        /// <returns>Результат проверки: true, если поля RawHash равны, false - в ином случае.</returns>
        public override bool Equals(object password)
        {
            if (password is Password)
            {
                if (((Password)password).RawHash.Length == RawHash.Length)
                {
                    for (int i = 0; i < ((Password)password).RawHash.Length; i++)
                    {
                        if (((Password)password).RawHash[i] != RawHash[i])
                            return false;
                    }

                    return true;
                }
            }

            if (password is byte[])
            {
                for (int i = 0; i < ((byte[])password).Length; i++)
                {
                    if (((byte[])password)[i] != RawHash[i])
                        return false;
                }

                return true;
            }

            return base.Equals(password);
        }

        /// <summary>
        /// Временный случайный пароль, автоматически сгенерированный в методе Generate.
        /// </summary>
        public char[] TemporaryPassword { get; }

        /// <summary>
        /// Генерация случайного пароля в соответствии с заданной конфигурацией параметров сложности пароля.
        /// </summary>
        /// <returns>Сгенерированный случайный пароль.</returns>
        char[] Generate()
        {
            if (Config.MinLength == 0)
            {
                return new char[0];
            }

            char[] lowers = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            char[] uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            char[] digits = "1234567890".ToCharArray();

            char[] allChars = lowers.Concat(uppers).Concat(digits).Concat(Config.SpecialSymbols).ToArray();

            byte[] data = GenerateRandom(Config.MinLength);

            StringBuilder result = new StringBuilder(Config.MinLength);

            foreach (byte b in data)
            {
                result.Append(allChars[b % (allChars.Length)]);
            }

            if (result.Length > 2)
            {
                if (Config.UseDigits && result.ToString().Where(s => char.IsDigit(s)).Count() == 0)
                {
                    result.Remove(result.Length - 1, 1);
                    result.Append(digits[data[0] % digits.Length]);
                }

                if (Config.UseUpperCase && result.ToString().Where(s => char.IsUpper(s)).Count() == 0)
                {
                    result.Remove(result.Length - 2, 1);
                    result.Insert(result.Length - 1, uppers[data[1] % uppers.Length]);
                }

                if (Config.UseSpecialSymbols && !result.ToString().Any(s => Config.SpecialSymbols.Contains(s)))
                {
                    result.Remove(result.Length - 3, 1);
                    result.Insert(result.Length - 2, Config.SpecialSymbols[data[2] % Config.SpecialSymbols.Length]);
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
            byte[] result = new byte[size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(result);
            }

            return result;
        }

        /// <summary>
        /// Создание хеша пароля, с использованием "соли" (синхропосылки) и локального параметра.
        /// </summary>
        /// <param name="clearText">Пароль для хеширования.</param>
        /// <returns>Хеш пароля.</returns>
        byte[] HashPassword(char[] clearText)
        {
            Encoding utf8 = Encoding.UTF8;
            byte[] hash;

            // создаем рабочий массив достаточного размера, чтобы вместить пароль, соль и локальный параметр
            byte[] data = new byte[utf8.GetMaxByteCount(clearText.Length)
                + RawSalt.Length
                + Config.LocalSalt.Length];

            try
            {
                // копируем пароль в рабочий массив, преобразуя его в UTF-8
                int byteCount = utf8.GetBytes(clearText, 0, clearText.Length, data, 0);

                // копируем синхропосылку в рабочий массив
                Array.Copy(RawSalt, 0, data, byteCount, RawSalt.Length);

                // копируем локальный параматр в рабочий массив
                Array.Copy(Config.LocalSalt, 0, data, byteCount + RawSalt.Length, Config.LocalSalt.Length);

                // хэшируем данные массива
                using (HashAlgorithm alg = new SHA512Managed())
                    hash = alg.ComputeHash(data, 0, RawSalt.Length + byteCount);
            }
            catch
            {
                throw;
            }
            finally
            {
                // очищаем рабочий массив в конце работы, чтобы избежать утечки открытого пароля
                Array.Clear(data, 0, data.Length);
            }

            return hash;
        }

        /// <summary>
        /// Изменение пароля.
        /// При равенстве нового и старого паролей поле ChangedAt не обновляется.
        /// </summary>
        /// <param name="newPasswordClearText">Текстовое представление нового пароляю</param>
        public void ChangeTo(char[] newPasswordClearText)
        {
            // Хеш нового пароля со старой "солью".
            var newRawHash = HashPassword(newPasswordClearText);
            
            // Если хеш нового пароля не равен старому хешу, то новый пароль отличается от старого. Создаем новый хеш.
            if (!Equals(newRawHash))
            {
                RawSalt = GenerateRandom(Config.SaltLength);
                RawHash = HashPassword(newPasswordClearText);
                ChangedAt = DateTime.UtcNow;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
