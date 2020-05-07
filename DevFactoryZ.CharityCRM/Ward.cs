using System;
using System.Collections.Generic;
using System.Linq;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Подопечный благотворительного фонда.
    /// </summary>
    public class Ward : IAmPersistent<int>
    {
        #region .ctor

        public Ward() // For ORM
        {
            CreatedAt = DateTime.UtcNow;
        }

        public Ward(FullName fullName, Address address, DateTime birthDate, string phone, IEnumerable<WardCategory> categories)
            :this()
        {
            FullName = fullName;
            Address = address;
            BirthDate = birthDate;
            Phone = phone;
            
            categories.Each(category => Grant(category));
        }

        public Ward(FullName fullName, Address address, DateTime birthDate, string phone, IEnumerable<WardCategory> categories, DateTime createdAt)
            : this(fullName, address, birthDate, phone, categories)
        {
            CreatedAt = createdAt;
        }

        #endregion

        #region Поля и свойства

        /// <summary>
        /// Представляет фамилию, имя, отчество подопечного БФ.
        /// </summary>
        public FullName FullName { get; }

        /// <summary>
        /// Представляет адресподопечного БФ
        /// </summary>
        public Address Address { get; }

        /// <summary>
        /// Возвращает дату регистрации подопечного в БФ.
        /// </summary>
        public DateTime CreatedAt { get; }

        /// <summary>
        /// Возвращает дату рождения подопечного БФ.
        /// </summary>
        public DateTime BirthDate { get; set; }

        public static bool PhoneIsRequired = false;

        public static int PhoneMaxLength = 12;

        private readonly RealString phone =
            new RealString(PhoneMaxLength, PhoneIsRequired, "номер телефона подопечного");

        /// <summary>
        /// Представляет номер телефона подопечного БФ.
        /// </summary>
        public string Phone { get => phone.Value; set => phone.Value = value; }

        /// <summary>
        /// Возвращает идентификатор подопечного в хранилище.
        /// </summary>
        public int Id { get; protected set; }

        public bool CanBeDeleted => true;

        #endregion

        #region Переопределенные методы

        public override string ToString()
        {
            return FullName.ToString();
        }

        #endregion

        #region Категории подопечного


        /// <summary>
        /// Тип для организации связи many-to-many Ward <--> WardCategory
        /// </summary>
        public class ThisWardCategory
        {
            protected ThisWardCategory() // for ORM
            {
            }

            /// <summary>
            /// Используется при добавлении/удалении категории подопечному БФ.
            /// </summary>
            /// <param name="wardCategory">Категория, которую требуется добавить/удалить подопечному БФ.</param>
            internal ThisWardCategory(WardCategory wardCategory)
            {
                WardCategory = wardCategory ?? throw new ArgumentNullException(nameof(wardCategory));
                WardCategoryId = WardCategory.Id;
            }

            /// <summary>
            /// Навигационное свойство для связи many-to-many Ward-WardCategory.
            /// Идентификатор подопечного БФ - владельца категории.
            /// </summary>
            public int WardId { get; set; }

            /// <summary>
            /// Навигационное свойство для связи many-to-many Ward-WardCategory.
            /// Идентификатор категории, содержащейся в свойстве ThisWardCategory.WardCategory.
            /// </summary>
            public int WardCategoryId { get; set; }

            /// <summary>
            /// Возвращает экземпляр категории, содержащейся в текущей ThisWardCategory.
            /// </summary>
            public WardCategory WardCategory { get; }

            #region Переопределенные методы для корректной работы HashSet<ThisWardCategory>()

            public override bool Equals(object obj)
            {
                return obj is ThisWardCategory anotherThisWardCategory
                    && anotherThisWardCategory.WardCategoryId == WardCategoryId;
            }

            public override int GetHashCode()
            {
                return WardCategoryId;
            }

            public static bool operator ==(ThisWardCategory left, ThisWardCategory right) => left.Equals(right);
            public static bool operator !=(ThisWardCategory left, ThisWardCategory right) => !(left == right);

            #endregion
        }

        /// <summary>
        /// Возвращает перечень категорий подопечного БФ.
        /// </summary>
        public HashSet<ThisWardCategory> WardCategories { get; } = new HashSet<ThisWardCategory>();

        /// <summary>
        /// Присваивает категорию подопечному БФ.
        /// </summary>
        /// <param name="wardCategory">Категория, которуе требуется присвоить подопечному.</param>
        public void Grant(WardCategory wardCategory)
        {
            WardCategories.Add(new ThisWardCategory(wardCategory));
        }

        /// <summary>
        /// Удаляет каегорию у подопечного БФ.
        /// </summary>
        /// <param name="wardCategory">Категория. которое требуется удалить.</param>
        public void Deny(WardCategory wardCategory)
        {
            WardCategories.Remove(new ThisWardCategory(wardCategory));
        }

        #endregion
    }
}

