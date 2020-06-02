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

        /// <summary>
        /// Для создания миграций
        /// </summary>
        public Ward()
        {
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Создает экземпляр <see cref="Ward"/>.
        /// Используется при создании нового подопечного.
        /// </summary>
        /// <param name="fullName">Фамилия, имя, отчество подопечного БФ.</param>
        /// <param name="address">Почтовый адрес подопечного БФ.</param>
        /// <param name="birthDate">Дата рождения подопечного БФ.</param>
        /// <param name="phone">Телефонный номер подопечного БФ.</param>
        /// <param name="categories">Катагории подопечного БФ.</param>
        public Ward(
            FullName fullName, 
            Address address, 
            DateTime birthDate, 
            string phone, 
            IEnumerable<WardCategory> categories)
            :this()
        {
            FullName = fullName;
            Address = address;
            BirthDate = birthDate;
            Phone = phone;
            
            categories.Each(category => AddCategory(category));
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

        /// <summary>
        /// Представляет номер телефона подопечного БФ.
        /// </summary>
        public string Phone { get => phone.Value; set => phone.Value = value; }

        public static bool PhoneIsRequired = false;

        public static int PhoneMaxLength = 12;

        private readonly RealString phone =
            new RealString(PhoneMaxLength, PhoneIsRequired, "номер телефона подопечного");

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
        /// Тип для организации связи many-to-many <see cref="Ward"/> <--> <see cref="CharityCRM.WardCategory"/>.
        /// </summary>
        public class WardCategoryCollectionElement
        {
            /// <summary>
            /// Для создания миграций
            /// </summary>        
            protected WardCategoryCollectionElement()
            {
            }

            /// <summary>
            /// Используется при добавлении/удалении категории <see cref="CharityCRM.WardCategory"/> 
            /// подопечному БФ.
            /// </summary>
            /// <param name="wardCategory">Категория <see cref="CharityCRM.WardCategory"/>, 
            /// которую требуется добавить/удалить подопечному БФ.</param>
            /// <param name="wardId">Идентификатор <see cref="Ward.Id"/> подопечного, для которого добавляется/удаляется 
            /// категория.</param>
            internal WardCategoryCollectionElement(WardCategory wardCategory, int wardId)
            {
                WardCategory = wardCategory ??
                    throw new ArgumentNullException(nameof(wardCategory));

                WardCategoryId = WardCategory.Id;

                WardId = wardId;
            }

            /// <summary>
            /// Навигационное свойство для связи many-to-many 
            /// <see cref="Ward"/> <--> <see cref="CharityCRM.WardCategory"/>.
            /// Идентификатор подопечного БФ - владельца категории.
            /// </summary>
            public int WardId { get; set; }

            /// <summary>
            /// Навигационное свойство для связи many-to-many 
            /// <see cref="Ward"/> <--> <see cref="CharityCRM.WardCategory"/>.
            /// Идентификатор категории, содержащейся в свойстве 
            /// <see cref="WardCategoryCollectionElement.WardCategory"/>.
            /// </summary>
            public int WardCategoryId { get; set; }

            /// <summary>
            /// Возвращает экземпляр категории, содержащейся в текущей 
            /// <see cref="WardCategoryCollectionElement"/>.
            /// </summary>
            public WardCategory WardCategory { get; }

            #region Переопределенные методы для корректной работы HashSet<WardCategoryCollectionElement>()

            public override bool Equals(object obj)
            {
                return obj is WardCategoryCollectionElement anotherThisWardCategory
                    && anotherThisWardCategory.WardCategoryId == WardCategoryId;
            }

            public override int GetHashCode()
            {
                return WardCategoryId.GetHashCode();
            }

            public static bool operator ==(WardCategoryCollectionElement left, 
                WardCategoryCollectionElement right) => left.Equals(right);
            
            public static bool operator !=(WardCategoryCollectionElement left, 
                WardCategoryCollectionElement right) => !(left == right);

            #endregion
        }

        /// <summary>
        /// Возвращает перечень категорий подопечного БФ.
        /// </summary>
        public HashSet<WardCategoryCollectionElement> WardCategories { get; } = 
            new HashSet<WardCategoryCollectionElement>();

        /// <summary>
        /// Присваивает категорию подопечному БФ.
        /// </summary>
        /// <param name="wardCategory">Категория, которуе требуется присвоить подопечному.</param>
        public void AddCategory(WardCategory wardCategory)
        {
            WardCategories.Add(new WardCategoryCollectionElement(wardCategory, Id));
        }

        /// <summary>
        /// Удаляет каегорию у подопечного БФ.
        /// </summary>
        /// <param name="wardCategory">Категория. которое требуется удалить.</param>
        public void RemoveCategory(WardCategory wardCategory)
        {
            var forRemove = WardCategories.First(c => c.WardCategory.Equals(wardCategory));
            
            forRemove.WardId = 0;

            WardCategories.Remove(forRemove);
        }

        #endregion
    }
}

