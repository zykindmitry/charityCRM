namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Этот класс представляет разрешение, требуемое для выполнение определенной операции
    /// </summary>
    public class Permission : IAmPersistent<int>
    {
        protected Permission()
        {
        }

        /// <summary>
        /// Создает экземпляр типа DevFactoryZ.CharityCRM.Permission
        /// </summary>
        /// <param name="name">Имя разрешения</param>
        /// <param name="description">Описание разрешения</param>
        public Permission(string name, string description) : this()
        {
        }

        /// <summary>
        /// Возвращает идентифтикатор разрешения в хранилище данных
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// Возвращает признак доступности цдаления разрешения из хранилища данных системы
        /// </summary>
        public bool CanBeDeleted => true;

        /// <summary>
        /// Возвращает имя разрешения
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Возвращает или задает описание разрешения
        /// </summary>
        public string Description { get; set; }
    }
}
