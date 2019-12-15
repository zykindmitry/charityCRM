using System;
using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Категория подопечного благотворительного фонда
    /// </summary>
    public class WardCategory
    {
        #region .ctor

        /// <summary>
        /// Создает экземпляр класса WardCategory
        /// </summary>
        /// <param name="name">Имя категории БФ</param>
        public WardCategory(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(
                        nameof(name),
                        "Название категории подопечного не может быть пустым.");
            }

            this.name = name;
        }

        #endregion

        #region Поля и свойства класса

        private string name;

        /// <summary>
        /// Свойство возвращает и записывает имя категории подопечного БФ.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(
                        nameof(value), 
                        "Название категории подопечного не может быть пустым.");
                }
                name = value;
            }
        }

        /// <summary>
        /// Возвращает/присваивает идентификатор гатегории БФ.
        /// </summary>
        public int Id { get; protected set; }
        
        /// <summary>
        /// Возвращает перечень дочерних категорий БФ.
        /// </summary>
        List<WardCategory> SubCategories { get; set; }

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
                    return false;
                }
            }
            return true;
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

        #endregion
    }
}
