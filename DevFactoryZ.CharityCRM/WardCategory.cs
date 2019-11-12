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
        }
        #endregion

        /// <summary>
        /// Имя категории подопечного БФ
        /// </summary>
        private string name;

        /// <summary>
        /// Свойство возвращает и записывает имя категории подопечного БФ
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
        public WardCategory Parent { get; set; }
        //Идентификатор категории
        public int Id { get; private set; }
        //Имя категории
        

        IEnumerable<WardCategory> SubCategories { get; }

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
        /// Добавляет подкатегорию подопечного БФ
        /// </summary>
        /// <param name="wardCategory">Объект категории подопечного БФ, которому будет присвоен в качестве родителя текущий экземпляр класса</param>
        public void AddChild(WardCategory wardCategory)
        {
            if (!wardCategory.Parent.Equals(this) && !IsCategoryEqualsAnyOfItsChildren(wardCategory))
            {
                wardCategory.Parent = this;
            }
        }

        public void RemoveChild(WardCategory wardCategory)
        {
            if (wardCategory.Parent.Equals(this))
            {
                wardCategory.Parent = null;
            }
        }
    }
}
