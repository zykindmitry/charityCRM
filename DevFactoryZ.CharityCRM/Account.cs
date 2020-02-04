using System;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Представляет учетную запись пользователя.
    /// Содержит методы для регистрации, аутентификации и авторизации пользователя в системе.
    /// </summary>
    public class Account : IAmPersistent<int>
    {
        #region .ctor

        /// <summary>
        /// Для создания миграции.
        /// </summary>
        protected Account()
        {
        }

        /// <summary>
        /// <para>ВНИМАНИЕ!!! ИСПОЛЬЗУЕТСЯ ДЛЯ РЕГИСТРАЦИИ НОВОГО ПОЛЬЗОВАТЕЛЯ.</para>
        /// Создает экземпляр Account с временным случайным паролем, сгенерированным в экземпляре <see cref="Password"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <param name="login">Имя пользователя.</param>
        /// <param name="passwordConfig">Конфигурация параметров сложности пароля.</param>
        public Account(
            string login
            , IPasswordConfig passwordConfig)
            : this(
                  login
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
        public Account(
            string login
            , char[] password
            , IPasswordConfig passwordConfig)
            : this(
                  login
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
        public Account(
            string login
            , Password password
            , DateTime? createdAt)
            : this()
        {
            Login = !string.IsNullOrWhiteSpace(login)
                ? login
                : throw new ArgumentNullException(nameof(login), "Имя пользователя не может быть пустым.");

            Password = password;
            CreatedAt = createdAt;
        }

        #endregion

        #region Данные учетной записи

        /// <summary>
        /// Возвращает уникальный идентификатор учетной записи
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// Для создания миграции - определяет, может ли <see cref="Login"/> быть <see cref="Nullable"/>.
        /// </summary>
        public static bool LoginIsRequired = true;

        /// <summary>
        /// Для создания миграции - максимальная длина <see cref="Login"/>.
        /// </summary>
        public static int LoginMaxLength = 15;

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
        /// Возвращает признак допустимости удаления учетной записи
        /// </summary>
        public bool CanBeDeleted => true;

        #endregion

        #region Аутентификация, авторизация

        /// <summary>
        /// Аутентификация пользователя. 
        /// </summary>
        /// <param name="passwordClearText">Пароль, введенный пользователем.</param>
        /// <returns>Результат проверки: true, если аутентификация успешна, false - в ином случае.</returns>
        public bool Authenticate(char[] passwordClearText)
        {
            var password = new Password(Password.PasswordConfig, passwordClearText, Password.RawSalt);

            return Password?.Equals(password) ?? throw new ValidationException("Отсутствует информация о пароле.");
        }

        /// <summary>
        /// Авторизация пользователя в системе.
        /// В текущей имплементации проверяется срок действия пароля.
        /// </summary>
        /// <returns>Результат проверки: true, если авторизация успешна, false - в ином случае.</returns>
        public bool Authorize()
        {
            return Password?.IsAlive ?? throw new ValidationException("Отсутствует информация о пароле.");
        }

        #endregion
    }
}
