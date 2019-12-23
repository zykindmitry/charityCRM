namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Представляет пожертвование в пользу одного из подопечных благотворительного фонда.
    /// </summary>
    public abstract class Donation : IAmPersistent<long>
    {
        #region .ctor

        protected Donation() // для ORM
        {
        }

        /// <summary>
        /// Создает экземпляр типа DevFactoryZ.CharityCRM.Donation.
        /// </summary>
        /// <param name="description">Описание пожертвования.</param>
        internal Donation(string description)
            : this()
        {
            Description = description;
        }

        #endregion

        #region Хранение и идентификация

        /// <summary>
        /// Возвращает идентификатор пожертвования, генерируемый на стороне хранилища.
        /// </summary>
        public long Id { get; protected set; }

        /// <summary>
        /// Возвращает признак доступности удаления пожертвования из хранилища данных системы.
        /// </summary>
        public bool CanBeDeleted => true;

        #endregion

        #region Описание пожертвования

        /// <summary>
        /// Возвращает описание пожертвования.
        /// </summary>
        public string Description { get; }

        public override string ToString()
        {
            return Description;
        }

        #endregion
    }
}
