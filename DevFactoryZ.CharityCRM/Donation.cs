namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Представляет пожертвование в пользу одного из подопечных благотворительного фонда.
    /// </summary>
    public class Donation : IAmPersistent<int>
    {
        #region .ctor

        protected Donation() // для ORM
        {
        }

        /// <summary>
        /// Создает экземпляр типа DevFactoryZ.CharityCRM.Donation.
        /// </summary>
        /// <param name="description">Описание пожертвования.</param>
        public Donation(string description)
            : this()
        {
            Description = description;
        }

        #endregion

        #region Хранение и идентификация

        /// <summary>
        /// Возвращает идентификатор пожертвования, генерируемый на стороне хранилища.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// Возвращает признак доступности удаления пожертвования из хранилища данных системы.
        /// </summary>
        public bool CanBeDeleted => true;

        public override bool Equals(object obj)
        {
            return (obj is Donation donation) && donation.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion

        #region Описание пожертвования

        /// <summary>
        /// Возвращает или задает описание пожертвования.
        /// </summary>
        public string Description { get; set; }

        public override string ToString()
        {
            return Description;
        }

        #endregion
    }
}
