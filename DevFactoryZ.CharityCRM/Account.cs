using System;

namespace DevFactoryZ.CharityCRM
{

    /// <summary>
    /// Представляет учетную запись пользователя.
    /// Содержит методы для регистрации, аутентификации и авторизации пользователя в системе.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// ВНИМАНИЕ!!! ИСПОЛЬЗУЕТСЯ ДЛЯ РЕГИСТРАЦИИ НОВОГО ПОЛЬЗОВАТЕЛЯ.
        /// Создает экземпляр Account с временным случайным паролем, сгенерированным в экземпляре Password.
        /// </summary>
        /// <param name="login">Имя пользователя.</param>
        /// <param name="passwordConfig">Конфигурация параметров сложности пароля.</param>
        public Account(string login, IPasswordConfig passwordConfig)
        {
            Login = !string.IsNullOrWhiteSpace(login)
                ? login
                : throw new ArgumentNullException(nameof(login), "Имя пользователя не может быть пустым.");

            Password = new Password(passwordConfig);
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// ВНИМАНИЕ!!! Используется при входе пользоателя в систему.
        /// Создает экземпляр Account c именем и паролем, введенными пользователем в экранной форме.
        /// </summary>
        /// <param name="login">Имя пользователя.</param>
        /// <param name="password">Текстовое представление пароля пользователя.</param>
        /// <param name="passwordConfig">Конфигурация параметров сложности пароля.</param>
        public Account(string login, char[] password, IPasswordConfig passwordConfig)
        {
            Login = !string.IsNullOrWhiteSpace(login) 
                ? login 
                : throw new ArgumentNullException(nameof(login), "Имя пользователя не может быть пустым.");
            
            Password = password != null
                ? new Password(password, passwordConfig) 
                : new Password(Array.Empty<char>(), passwordConfig);            
        }

        /// <summary>
        /// Создает экземпляр Account.
        /// Используется при получении данных из репозитория аккаунтов.
        /// </summary>
        /// <param name="Login">Имя пользователя.</param>
        /// <param name="password">Экземпляр Password.</param>
        /// <param name="createdAt">Время создания аккаунта.</param>
        public Account(string login, Password password, DateTime? createdAt)
        {
            Login = login;
            Password = password;
            CreatedAt = createdAt;
        }

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

        /// <summary>
        /// Аутентификация пользователя в указанном репозитории аккаунтов. 
        /// Если в указанном репозитории существует пользователь с именем, указанным в поле Login текущего экземпляра Account,
        /// и хеш указанного пароля (с солью из репозитория) совпадает с хешем пароля в репозитории, то аутентификация успешна.
        /// </summary>
        /// <param name="repository">Репозиторий аккаунтов, в котором производится аутентификация текущего пользователя.</param>
        /// <param name="passwordClearText">Пароль, введенный пользователем.</param>
        /// <param name="errorText">string.Empty, если аутентификация успешна, текст ошибки - в ином случае.</param>
        /// <returns>Результат проверки: true, если аутентификация успешна, false - в ином случае.</returns>
        public bool IsAuthenticated(Persistence.IAccountRepository repository, char[] passwordClearText, out string errorText)
        {
            errorText = string.Empty;
            var fromRepository = repository.GetByLogin(Login);

            if (fromRepository == null)
            {
                errorText = "Неверное имя пользователя.";

                return false;
            }

            var password = new Password(passwordClearText, fromRepository.Password.Config, fromRepository.Password.Salt);
            if (!fromRepository.Password.Equals(password))
            {
                errorText = "Неверный пароль.";

                return false;
            }

            return true;
        }

        /// <summary>
        /// Авторизация пользователя в системе.
        /// ВНИМАНИЕ!!! В текущей реализации критерии авторизации не определены, поэтому всегда возвращает true.
        /// </summary>
        /// <param name="repository">Репозиторий аккаунтов, в котором производится авторизация текущего пользователя.</param>
        /// <param name="errorText">string.Empty, если авторизация успешна, текст ошибки - в ином случае.</param>
        /// <returns>Результат проверки: true, если авторизация успешна, false - в ином случае.</returns>
        public bool IsAuthorized(Persistence.IAccountRepository repository, out string errorText)
        {
            errorText = string.Empty;
            var fromRepository = repository.GetByLogin(Login);

            return true;
        }

    }
}
