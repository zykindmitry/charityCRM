using System;
using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Категория подопечного благотворительного фонда
    /// </summary>
    class WardCategory
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
                throw new ArgumentNullException(nameof(name), "Название категории подопечный благотворительного фонда " +
                    "не может быть пустым.");
            }

            this.name = name;
            Id = Guid.NewGuid();
        }
        #endregion

        #region Поля и свойства класса
        /// <summary>
        /// Имя категории подопечного БФ.
        /// </summary>
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
                    throw new ArgumentNullException(nameof(value), "Название категории подопечный благотворительного фонда " +
                        "не может быть пустым.");
                }
                name = value;
            }
        }

        /// <summary>
        /// Возвращает/присваивает родительскую категорию БФ.
        /// </summary>
        public WardCategory Parent { get; private set; }

        /// <summary>
        /// Возвращает/присваивает идентификатор гатегории БФ.
        /// </summary>
        public Guid Id { get; private set; }
        
        /// <summary>
        /// Возвращает перечень дочерних категорий БФ.
        /// </summary>
        IEnumerable<WardCategory> SubCategories { get; }
        #endregion

        #region Методы класса

        /// <summary>
        /// Внутренний метод, проверяющий совпадает ли категория с любой из ее подкатегорий.
        /// </summary>
        /// <param name="wardCategory">Категория подопечного БФ.</param>
        /// <returns>true/false</returns>
        private bool IsCategoryEqualsAnyOfItsChildren(WardCategory wardCategory)
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
        /// Добавляет подкатегорию подопечного БФ.
        /// </summary>
        /// <param name="wardCategory">Объект категории подопечного БФ, которому будет присвоен в качестве родителя текущий экземпляр класса</param>
        public void AddChild(WardCategory wardCategory)
        {
            if (!wardCategory.Parent.Equals(this) && !IsCategoryEqualsAnyOfItsChildren(wardCategory))
            {
                wardCategory.Parent = this;
            }
        }

        /// <summary>
        /// Удаляет информацию о родителе переданной в параметре категории БФ.
        /// </summary>
        /// <param name="wardCategory"></param>
        public void RemoveChild(WardCategory wardCategory)
        {
            if (wardCategory.Parent.Equals(this))
            {
                wardCategory.Parent = null;
            }
        }
        #endregion
    }
}
