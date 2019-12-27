using System;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Этот класс представляет разрешение, требуемое для выполнение определенной операции
    /// </summary>
    public class Permission : IAmPersistent<int>
    {
        #region .ctor

        protected Permission() // для ORM
        {
        }

        /// <summary>
        /// Создает экземпляр типа DevFactoryZ.CharityCRM.Permission
        /// </summary>
        /// <param name="name">Имя разрешения</param>
        /// <param name="description">Описание разрешения</param>
        public Permission(string name, string description) : this()
        {
            Name =
                !string.IsNullOrWhiteSpace(name) 
                ? name
                : throw new ArgumentException("Имя разрешения не может быть пустым.", nameof(name));

            Description = description;
        }

        #endregion

        #region Хранение и идентификация

        /// <summary>
        /// Возвращает идентифтикатор разрешения в хранилище данных
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// Возвращает признак доступности удаления разрешения из хранилища данных системы
        /// </summary>
        public bool CanBeDeleted => true;

        public override bool Equals(object obj)
        {
            return (obj is Permission permission) && permission.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion

        #region Описание разрешения

        public static bool NameIsRequired = true;

        public static int NameMaxLength = 100;

        private readonly RealString name = 
            new RealString(NameMaxLength, NameIsRequired, "наименование разрешения");

        /// <summary>
        /// Возвращает или задает имя разрешения
        /// </summary>
        public string Name { get => name.Value; set => name.Value = value; }

        /// <summary>
        /// Возвращает или задает описание разрешения
        /// </summary>
        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
