using System;
using System.Collections.Generic;
using System.Linq;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Категория подопечного благотворительного фонда
    /// </summary>
    public class WardCategory : IAmPersistent<int>
    {
        #region .ctor

        /// <summary>
        /// Для создания миграций
        /// </summary>
        public WardCategory()
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
                : throw new ArgumentNullException( nameof(name), 
                    "Наименование категории подопечного не может быть пустым.");
        }

        #endregion

        #region Поля и свойства класса

        /// <summary>
        /// Возвращает или задает имя роли
        /// </summary>
        public string Name { get => name.Value; set => name.Value = value; }

        public static bool NameIsRequired = true;

        public static int NameMaxLength = 30;

        private readonly RealString name =
            new RealString(NameMaxLength, NameIsRequired, "наименование категории подопечного");

        /// <summary>
        /// Возвращает/присваивает идентификатор категории БФ.
        /// </summary>
        public int Id { get; set; }
        
        public bool CanBeDeleted => true;

        #endregion

        #region Дочерние подкатегории

        /// <summary>
        /// Возвращает перечень подкатегорий для текущей категории подопечного БФ.
        /// </summary>
        public HashSet<WardCategory> SubCategories { get; } = new HashSet<WardCategory>();

        /// <summary>
        /// Добавляет подкатегорию подопечного.
        /// </summary>
        /// <param name="wardCategory">Объект категории подопечного БФ, которому будет присвоен в качестве родителя текущий экземпляр класса</param>
        public void AddChild(WardCategory wardCategory)
        {
            if (Id == wardCategory.Id)
            {
                throw new ArgumentException(
                    $"Циклическая ссылка. Категории не могут быть родителями сам себе.", 
                        nameof(wardCategory));
            }

            if (wardCategory.SubCategories.Any(s => s.Equals(this)))
            {
                throw new ArgumentException(
                    $"Циклическая ссылка. Категория не может одновременно быть родителем и подкатегорией для другой категории.", nameof(wardCategory));
            }

            if (!SubCategories.Add(wardCategory))
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
            if (!SubCategories.Remove(wardCategory))
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
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }

        public static bool operator ==(WardCategory left, WardCategory right) =>
            left.Equals(right);

        public static bool operator !=(WardCategory left, WardCategory right) => 
            !(left == right);

        #endregion
    }
}
