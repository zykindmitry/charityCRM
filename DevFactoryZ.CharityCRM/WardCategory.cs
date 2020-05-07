using System;
using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Категория подопечного благотворительного фонда
    /// </summary>
    public class WardCategory : IAmPersistent<int>
    {
        #region .ctor

        public WardCategory() // For ORM
        {

        }

        /// <summary>
        /// Создает экземпляр класса WardCategory
        /// </summary>
        /// <param name="name">Наимнование категории подопечного БФ</param>
        public WardCategory(string name)
        {
            Name = !string.IsNullOrWhiteSpace(name)
                ? name
                : throw new ArgumentNullException( nameof(name), "Наименование категории подопечного не может быть пустым.");
        }

        #endregion

        #region Поля и свойства класса

        public static bool NameIsRequired = true;

        public static int NameMaxLength = 30;

        private readonly RealString name =
            new RealString(NameMaxLength, NameIsRequired, "наименование категории подопечного");

        /// <summary>
        /// Возвращает или задает имя роли
        /// </summary>
        public string Name { get => name.Value; set => name.Value = value; }

        /// <summary>
        /// Возвращает/присваивает идентификатор категории БФ.
        /// </summary>
        public int Id { get; protected set; }
        
        public bool CanBeDeleted => true;

        #endregion

        #region Дочерние подкатегории

        /// <summary>
        /// Тип для организации связи many-to-many WardCatgory <--> WardCategory
        /// Используется для построения иерархической структуры категорий подопечного БФ.
        /// </summary>
        public class WardCategorySubCategory
        {
            protected WardCategorySubCategory() // for ORM
            {
            }

            /// <summary>
            /// Используется при добавлении/удалении подкатегории в/из категории.
            /// </summary>
            /// <param name="wardCategory">Подкатегория, которую требуется добавить/удалить в/из категории.</param>
            internal WardCategorySubCategory(WardCategory wardCategory)
            {
                WardCategory = wardCategory ?? throw new ArgumentNullException(nameof(wardCategory));
                SubCategoryId = WardCategory.Id;
            }

            /// <summary>
            /// Навигационное свойство для связи many-to-many WardCatgory <--> WardCategory.
            /// Идентификатор подкатегории, содержащейся в свойстве WardCategorySubCategory.WardCategory
            /// </summary>
            public int SubCategoryId { get; set; }

            /// <summary>
            /// Навигационное свойство для связи many-to-many WardCategory <--> WardCategory.
            /// Идентификатор родительского WardCategory
            /// </summary>
            public int WardCategoryId { get; set; }

            /// <summary>
            /// Возвращает экземпляр подкатегории, содержащейся в текущей WardCategorySubCategory
            /// </summary>
            public WardCategory WardCategory { get; }

            #region Переопределенные методы для корректной работы HashSet<WardCategorySubCategory>()

            public override bool Equals(object obj)
            {
                return 
                    obj is WardCategorySubCategory anotherCategorySubCategory
                    && anotherCategorySubCategory.SubCategoryId == SubCategoryId;
            }

            public override int GetHashCode()
            {
                return SubCategoryId;
            }

            public static bool operator ==(WardCategorySubCategory left, WardCategorySubCategory right) => left.Equals(right);
            public static bool operator !=(WardCategorySubCategory left, WardCategorySubCategory right) => !(left == right);

            #endregion
        }

        /// <summary>
        /// Возвращает перечень подкатегорий для текущей категории подопечного БФ.
        /// </summary>
        public HashSet<WardCategorySubCategory> SubCategories { get; } = new HashSet<WardCategorySubCategory>();

        #endregion

        #region Методы класса

        /// <summary>
        /// Добавляет подкатегорию подопечного.
        /// </summary>
        /// <param name="wardCategory">Объект категории подопечного БФ, которому будет присвоен в качестве родителя текущий экземпляр класса</param>
        public void AddChild(WardCategory wardCategory)
        {
            if (!SubCategories.Add(new WardCategorySubCategory(wardCategory)))
            {
                throw new InvalidOperationException(
                    $"Категория '{wardCategory.Id} ({wardCategory.Name})' уже входит в список подкатегорий для категории '{Id} ({Name})'.");
            }
        }

        /// <summary>
        /// Удаляет подкатегорию подопечного.
        /// </summary>
        /// <param name="wardCategory">Объект подкатегории подопечного для удаления из родительской категории.</param>
        public void RemoveChild(WardCategory wardCategory)
        {
            if (!SubCategories.Remove(new WardCategorySubCategory(wardCategory)))
            {
                throw new InvalidOperationException(
                    $"Категория '{wardCategory.Id} ({wardCategory.Name})' не входит в список подкатегорий для категории '{Id} ({Name})'.");
            }
        }

        #endregion

        #region Переопределенные методы

        public override bool Equals(object obj)
        {
            return 
                obj is WardCategory anotherCategory 
                && anotherCategory.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
