using System;
using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM
{

    /// <summary>
    /// Представляет учетную запись пользователя.
    /// Содержит методы для регистрации, аутентификации и авторизации пользователя в системе.
    /// </summary>
    public class Account
    {
        #region .ctor

        /// <summary>
        /// <para>ВНИМАНИЕ!!! ИСПОЛЬЗУЕТСЯ ДЛЯ РЕГИСТРАЦИИ НОВОГО ПОЛЬЗОВАТЕЛЯ.</para>
        /// Создает экземпляр Account с временным случайным паролем, сгенерированным в экземпляре <see cref="Password"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <param name="login">Имя пользователя.</param>
        /// <param name="passwordConfig">Конфигурация параметров сложности пароля.</param>
        public Account(string login, IPasswordConfig passwordConfig) 
            : this(login
                  , new Password(passwordConfig)
                  , DateTime.UtcNow)
        {

        }

        /// <summary>
        /// <para>ВНИМАНИЕ!!! Используется при первом входе пользоателя в систему или смене пароля.</para>
        /// Создает экземпляр Account c именем и паролем, введенными пользователем в экранной форме.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <param name="login">Имя пользователя.</param>
        /// <param name="password">Текстовое представление пароля пользователя.</param>
        /// <param name="passwordConfig">Конфигурация параметров сложности пароля.</param>
        public Account(string login, char[] password, IPasswordConfig passwordConfig) 
            : this(login
                  , new Password(passwordConfig, password ?? Array.Empty<char>())
                  , null)
        {            

        }

        /// <summary>
        /// <para>Создает экземпляр Account с заданными параметрами.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <param name="Login">Имя пользователя.</param>
        /// <param name="password">Экземпляр Password.</param>
        /// <param name="createdAt">Время создания аккаунта.</param>
        public Account(string login, Password password, DateTime? createdAt)
        {
            Login = !string.IsNullOrWhiteSpace(login)
                ? login
                : throw new ArgumentNullException(nameof(login), "Имя пользователя не может быть пустым.");
            
            Password = password;
            CreatedAt = createdAt;
        }

        #endregion


        /// <summary>
        /// Имя пользователя в системе.
        /// </summary>
        public string Login { get; }
        
        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public Password Password { get; }
        
        /// <summary>
        /// Дата создания аккаунта в формате UTC.
        /// </summary>
        public DateTime? CreatedAt { get; }


        #region Аутентификация, авторизация

        /// <summary>
        /// Аутентификация пользователя. 
        /// </summary>
        /// <param name="passwordClearText">Пароль, введенный пользователем.</param>
        /// <param name="errorText">string.Empty, если аутентификация успешна, текст ошибки - в ином случае.</param>
        /// <returns>Результат проверки: true, если аутентификация успешна, false - в ином случае.</returns>
        public bool Authenticate(char[] passwordClearText, out string errorText)
        {
            errorText = string.Empty;

            var password = new Password(Password.Config, passwordClearText, Password.Salt);

            if (!Password.Equals(password))
            {
                errorText = "Неверный пароль.";

                return false;
            }

            return true;
        }

        /// <summary>
        /// Авторизация пользователя в системе.
        /// В текущей имплиментации проверяется срок действия пароля.
        /// </summary>
        /// <param name="errorText">string.Empty, если авторизация успешна, текст ошибки - в ином случае.</param>
        /// <returns>Результат проверки: true, если авторизация успешна, false - в ином случае.</returns>
        public bool Authorize(out string errorText)
        {
            errorText = string.Empty;

            if (!Password.IsAlive)
            {
                errorText = "Срок действия пароля истек.";

                return false;
            }

            return true;
        }

        #endregion
    }
}
