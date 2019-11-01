using System;
using System.Collections.Generic;
using System.Text;

namespace DevFactoryZ.CharityCRM
{
    public class FundRegistration
    {
        #region .ctor and properties

        public FundRegistration(string name, string description, TimeSpan linkMaxLifeTime)
        {
            this.name = name;
            this.description = description;
            this.registrationLinkMaxLifeTime = linkMaxLifeTime;
        }

        string name;
        /// <summary>
        /// Наименование фонда. Передается в конструкторе класса.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        string description;
        /// <summary>
        /// Краткое описание. Может быть пустым. Передается в конструкторе класса.
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        string registrationLink;
        /// <summary>
        /// Переменная часть ссылки на регистрационную форму. 
        /// Создается в момент регистрации заявки на открытие нового фонда в системе, в методе CreateRegistrationLink. 
        /// Возвращается вызовом метода TryGetRegistrationLink.
        /// </summary>
        string RegistrationLink
        {
            get
            {
                return registrationLink;
            }
        }

        DateTime registrationLinkCreatedTimeUTC;
        /// <summary>
        /// Дата и время создания ссылки на регистрационную форму. 
        /// Создается в момент регистрации заявки на открытие нового фонда в системе, в методе CreateRegistrationLink. 
        /// Используется для определения валидности ссылки в методе TryGetRegistrationLink.
        /// </summary>
        DateTime RegistrationLinkCreatedTimeUTC
        {
            get
            {
                return registrationLinkCreatedTimeUTC;
            }
        }

        TimeSpan registrationLinkMaxLifeTime;
        /// <summary>
        /// Максимальная продолжительность жизни ссылки на регистрационную форму. Передается в конструкторе класса.
        /// Используется для определения валидности ссылки в методе TryGetRegistrationLink.
        /// </summary>
        TimeSpan RegistrationLinkMaxLifeTime
        {
            get
            {
                return registrationLinkMaxLifeTime;
            }
        }
        #endregion

        #region methods

        /// <summary>
        /// Создание переменной части ссылки на регистрационную форму.
        /// Вызывать в момент регистрации заявки на открытие нового фонда в системе.
        /// </summary>
        public void CreateRegistrationLink()
        {
            registrationLink = System.Guid.NewGuid().ToString();
            registrationLinkCreatedTimeUTC = DateTime.UtcNow;
        }

        /// <summary>
        /// Проверяет валидность переменной части ссылки на регистрационную форму и возвращает результат проверки.
        /// Если с момента создания ссылки прошло больше времени, чем заданная в свойстве RegistrationLinkMaxLifeTime продолжительность жизни ссылки, 
        /// то возвращает false и пустую строку.
        /// В противном случае возвращает true и ссылку.
        /// </summary>
        /// <param name="link">Переменная часть ссылки на регистрационную форму.</param>
        /// <returns>Результат проверки валидности ссылки.</returns>
        public bool TryGetRegistrationLink(out string link)
        {
            if (DateTime.UtcNow.Subtract(registrationLinkCreatedTimeUTC) > registrationLinkMaxLifeTime)
            {
                link = string.Empty;
                return false;
            }
                        
            link = this.registrationLink;
            return true;
        }
        #endregion
    }
}
