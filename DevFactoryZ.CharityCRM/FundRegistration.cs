using System;

namespace DevFactoryZ.CharityCRM
{

    /// <summary>
    /// Представляет заявку на создание (регистрацию) фонда в системе.
    /// Содержит необходимые первичные данные: 
    ///  - название фонда;
    ///  - пояснения/комментарии текущего обработчика заявки (при необходимости); 
    ///  - уникальная (в рамках данного решения) переменная часть (идентификатор) ссылки на регистрационную форму. Вся ссылка, содержащая, в т.ч. и идентификатор, отправляется инициатору создания фонда (для заполнения регистрационной формы) пользователем данного класса;
    ///  - дата/время создания идентификатора ссылки на регистрационную форму;
    ///  - максимальная продолжительность жизни ссылки на регистрационную форму. Если интервал времени от момента создания идентификатора ссылки до момента DateTime.Now превышает это значение, то ссылка теряет актуальность и не подлежит дальнейшему использованию.
    /// Содержит методы для создания и валидации идентификатора ссылки на регистрационную форму.
    /// Содержит дату/время обработки текущей заявки иницитором, т.е. дату/время создания (регистрации) фонда в системе.
    /// </summary>
    public class FundRegistration
    {
        #region .ctor

        /// <summary>
        /// Создает экземпляр типа FundRegistration. 
        /// Создает идентификатор ссылки на регистрационную форму, фиксирует дату/время создания идентификатора.
        /// </summary>
        /// <param name="name">Название фонда.</param>
        /// <param name="description">Краткое описание/пояснения/комментарии к заявке.</param>
        /// <param name="linkMaxLifeTime">Максимальная продолжительность жизни ссылки на регистрационную форму.</param>
        public FundRegistration(string name, string description, TimeSpan linkMaxLifeTime)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Название фонда не может быть пустым.");
            }

            Name = name;
            
            Description = description;
            
            RegistrationLinkMaxLifeTime = linkMaxLifeTime;

            RegistrationLinkGUID = System.Guid.NewGuid();

            RegistrationLinkGUIDCreatedTimeUTC = DateTime.UtcNow;
        }

        #endregion

        #region Первичные данные для регистрации

        /// <summary>
        /// Наименование фонда. Передается в конструкторе класса.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Краткое описание/пояснение к заявке. Может быть пустым. Передается в конструкторе класса.
        /// Можеь быть изменено на протяжении жизненного цикла заявки текущим обработчиком заявки.
        /// </summary>
        public string Description { get; set; }

        #endregion

        #region Обработка идентификатора ссылки на регистрационную форму. Хранение, проверка валмдности.

        /// <summary>
        /// Идентификатор ссылки на регистрационную форму. 
        /// Инициализируется в конструкторе в момент создания заявки на регистрацию нового фонда в системе. 
        /// </summary>
        public Guid RegistrationLinkGUID { get; }

        /// <summary>
        /// Дата и время создания ссылки на регистрационную форму. 
        /// Инициализируется в конструкторе в момент создания заявки на регистрацию нового фонда в системе. 
        /// Используется для определения валидности ссылки.
        /// </summary>
        DateTime RegistrationLinkGUIDCreatedTimeUTC { get; }

        /// <summary>
        /// Максимальная продолжительность жизни ссылки на регистрационную форму. Передается в конструкторе класса.
        /// Используется для определения валидности ссылки.
        /// </summary>
        TimeSpan RegistrationLinkMaxLifeTime { get; }

        /// <summary>
        /// Проверяет валидность ссылки на регистрационную форму и возвращает результат проверки.
        /// Если с момента создания идентификатора ссылки прошло больше времени, чем заданная в свойстве RegistrationLinkMaxLifeTime продолжительность жизни ссылки, 
        /// либо значение DateOfRegistration больше DateTime.MinValue и меньше DateTime.UtcNow, то возвращает false.
        /// В противном случае возвращает true.
        /// </summary>
        /// <returns>Результат проверки валидности ссылки.</returns>
        public bool IsValid()
        {
            if (DateTime.UtcNow.Subtract(RegistrationLinkGUIDCreatedTimeUTC) > RegistrationLinkMaxLifeTime)
            {
                return false;
            }

            if (DateOfRegistration < DateTime.UtcNow && DateOfRegistration > DateTime.MinValue)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Информация о регистрации фонда

        /// <summary>
        /// Дата/время обработки текущей заявки иницитором, т.е. дата/время создания (регистрации) фонда в системе.
        /// </summary>
        public DateTime DateOfRegistration { get; }

        #endregion
    }
}
