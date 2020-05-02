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
        /// <param name="name">Имя категории БФ</param>
        public WardCategory(string name)
        {
            Name = !string.IsNullOrWhiteSpace(name)
                ? name
                : throw new ArgumentNullException( nameof(name), "Название категории подопечного не может быть пустым.");
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
        /// Возвращает/присваивает идентификатор гатегории БФ.
        /// </summary>
        public int Id { get; protected set; }
        
        /// <summary>
        /// Возвращает перечень дочерних категорий БФ.
        /// </summary>
        public List<WardCategory> SubCategories { get; set; }

        public bool CanBeDeleted => true;

        #endregion

        #region Методы класса

        /// <summary>
        /// Внутренний метод, проверяющий, является ли категория потомком текущей категории.
        /// </summary>
        /// <param name="wardCategory">Категория подопечного.</param>
        /// <returns>true/false</returns>
        private bool IsChild(WardCategory wardCategory)
        {
            foreach (var item in SubCategories)
            {
                if (item.Equals(wardCategory))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Добавляет подкатегорию подопечного.
        /// </summary>
        /// <param name="wardCategory">Объект категории подопечного БФ, которому будет присвоен в качестве родителя текущий экземпляр класса</param>
        public void AddChild(WardCategory wardCategory)
        {
            if (!wardCategory.IsChild(this))
            {
                this.SubCategories.Add(wardCategory);
            }
        }

        /// <summary>
        /// Удаляет подкатегорию подопечного.
        /// </summary>
        /// <param name="wardCategory"></param>
        public void RemoveChild(WardCategory wardCategory)
        {
            if (wardCategory.IsChild(this))
            {
                this.SubCategories.Remove(wardCategory);
            }
        }

        #endregion

        #region Переопределенные методы

        public override bool Equals(object obj)
        {
            var anotherCategory = obj as WardCategory;

            return anotherCategory != null && anotherCategory.Id == Id;
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
