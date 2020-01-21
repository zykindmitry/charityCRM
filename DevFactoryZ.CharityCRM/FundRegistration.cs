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
    public class FundRegistration : IAmPersistent<Guid>
    {
        #region .ctor

        /// <summary>
        /// Создает экземпляр типа FundRegistration. 
        /// Создает идентификатор ссылки на регистрационную форму, фиксирует дату/время создания идентификатора.
        /// </summary>
        /// <param name="name">Название фонда.</param>
        /// <param name="description">Краткое описание/пояснения/комментарии к заявке.</param>
        /// <param name="maxLifeTime">Максимальная продолжительность жизни заявки.</param>
        public FundRegistration(string name, string description, TimeSpan maxLifeTime)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Название фонда не может быть пустым.");
            }

            Name = name;
            Description = description;

            MaxLifeTime = maxLifeTime;
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        #endregion

        #region Первичные данные для регистрации

        /// <summary>
        /// Возврашает наименование БФ. 
        /// </summary>
        public static bool NameIsRequired = true;

        public static int NameMaxLength = 100;

        private readonly RealString name =
            new RealString(NameMaxLength, NameIsRequired, "наименование БФ");

        /// <summary>
        /// Возвращает или задает имя БФ
        /// </summary>
        public string Name { get => name.Value; protected set => name.Value = value; }

        /// <summary>
        /// Возращает или задает описание/пояснение к заявке на регистрацию БФ. Может быть пустым или null 
        /// </summary>
        public string Description { get; set; }

        #endregion

        #region Реализация интерфейса IAmPersistent<Guid>

        /// <summary>
        /// Возвращает идентификатор данной заявки на регистрацию БФ
        /// Инициализируется в конструкторе. 
        /// </summary>
        public Guid Id { get; protected set; }

        /// <summary>
        /// Возвращает признак возможности удаления заявки на регистрацию БФ из хранилища данных
        /// </summary>
        public bool CanBeDeleted => !Succeeded;

        #endregion

        #region Обработка идентификатора ссылки на регистрационную форму. Хранение, проверка валмдности.

        /// <summary>
        /// Возвращает дату и время создания заявки на регистрацию БФ в системе в UTC
        /// Инициализируется в конструкторе текущей датой в UTC
        /// </summary>
        public DateTime CreatedAt { get; }

        /// <summary>
        /// Возвращает интервал времени доступности ссылки на регистрацию фонда
        /// Инициализируется в конструкторе. 
        /// </summary>
        public TimeSpan MaxLifeTime { get; }

        private bool TimedOut => DateTime.UtcNow.Subtract(CreatedAt) <= MaxLifeTime;

        /// <summary>
        /// Возвращает признак доступности данной заявки на регистрацию БФ для регистрации
        /// Заявка доступна если она еще не просрочена (см. CreatedAt и MaxLifeTime) 
        /// и еще не была использована (см. SucceededAt)
        /// </summary>
        /// <returns>Результат проверки валидности ссылки.</returns>
        public bool CanBeSucceeded => !(!TimedOut || Succeeded);

        #endregion

        #region Информация о регистрации фонда по текущей заявке

        private bool Succeeded => SucceededAt.HasValue;

        /// <summary>
        /// Возращает дату и время успешной регистрации фонда по данной заявке инициатором в UTC.
        /// </summary>
        public DateTime? SucceededAt { get; private set; }

        public void Succeed()
        {
            if (SucceededAt != null)
            {
                if (SucceededAt <= DateTime.UtcNow)
                {
                    throw new InvalidOperationException($"Заявка уже обработана. Дата обработки: {SucceededAt}.");
                }
                else
                {
                    throw new InvalidOperationException($"Некорректная дата обработки заявки: {SucceededAt}.");
                }
            }

            SucceededAt = DateTime.UtcNow;
        }
        
        #endregion
    }
}
