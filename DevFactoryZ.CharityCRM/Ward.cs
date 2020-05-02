using System;
using System.Collections.Generic;
using System.Linq;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Подопечный фонда
    /// </summary>
    public class Ward : IAmPersistent<int>
    {
        #region .ctor

        public Ward()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public Ward(FIO fio, Address address, DateTime birthDate, string phone, IEnumerable<WardCategory> categories)
            :this()
        {
            FIO = fio;
            Address = address;
            BirthDate = birthDate;
            Phone = phone;

            categories.Each(category => Grant(category));
        }

        public Ward(FIO fio, Address address, DateTime birthDate, string phone, IEnumerable<WardCategory> categories, DateTime createdAt)
            : this(fio, address, birthDate, phone, categories)
        {
            CreatedAt = createdAt;
        }

        #endregion

        #region Поля и свойства

        public FIO FIO { get; set; }

        public Address Address { get; set; }

        public DateTime CreatedAt { get; }

        public DateTime BirthDate { get; set; }
        
        public string Phone { get; set; }

        public int Id { get; protected set; }

        public bool CanBeDeleted => true;

        #endregion

        #region Методы
        #endregion

        #region Переопределенные методы

        public override string ToString()
        {
            return FIO.FullName;
        }

        #endregion

        #region Категории подопечного

        public class ThisWardCategory
        {
            protected ThisWardCategory() // for ORM
            {
            }

            internal ThisWardCategory(WardCategory wardCategory)
            {
                WardCategory = wardCategory;
            }

            /// <summary>
            /// Навигационное свойство для связки Ward-ThisWardCategory.
            /// EF сам почему-то не хочет создавать теневое свойство, хотя в аналогичной конфигурации Role-RolePermission теневое свойство создается.
            /// </summary>
            public int WardId { get; set; }

            public WardCategory WardCategory { get; protected set; }
        }

        public static string ThisWardCategoriesFieldName => nameof(wardCategories);

        private readonly List<ThisWardCategory> wardCategories = new List<ThisWardCategory>();

        /// <summary>
        /// Возвращает коллекцию категорий подопечного
        /// </summary>
        public IEnumerable<ThisWardCategory> WardCategories => wardCategories.AsReadOnly();

        /// <summary>
        /// Присваивает категорию подопечному
        /// </summary>
        /// <param name="wardCategory">Категория, которое нужно присвоить подопечному</param>
        public void Grant(WardCategory wardCategory)
        {
            if (wardCategories
                .Any(x => x.WardCategory.Equals(WardCategoryOrNullException(wardCategory))))
            {
                throw new InvalidOperationException(
                    $"Подопечный {this} уже имеет категорию {wardCategory}");
            }

            wardCategories.Add(new ThisWardCategory(wardCategory));
        }

        /// <summary>
        /// Удаляет каегорию у подопечного
        /// </summary>
        /// <param name="wardCategory">Категория. которое необходимо удалить</param>
        public void Deny(WardCategory wardCategory)
        {
            var thisWardCategory = wardCategories
                .FirstOrDefault(x => x.WardCategory.Equals(WardCategoryOrNullException(wardCategory)));

            if (thisWardCategory == null)
            {
                throw new InvalidOperationException($"Подопечный {this} не имеет категории {wardCategory}");
            }

            wardCategories.Remove(thisWardCategory);
        }

        private WardCategory WardCategoryOrNullException(WardCategory wardCatewgory)
        {
            return wardCatewgory ?? throw new ArgumentNullException(nameof(wardCatewgory));
        }

        #endregion
    }
}

