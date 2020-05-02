namespace DevFactoryZ.CharityCRM
{
    public class FIO : IFIO
    {
        #region .ctor
        
        /// <summary>
        /// Для создания миграций
        /// </summary>
        public FIO()
        {

        }

        /// <summary>
        /// Создает экземпляр <see cref="FIO"/>
        /// </summary>
        /// <param name="lastName">Фамилия</param>
        /// <param name="firstName">Имя</param>
        /// <param name="midName">Отчество</param>
        public FIO(string lastName, string firstName, string midName) : this()
        {
            FirstName = firstName.Trim();
            MidName = midName.Trim();
            LastName = lastName.Trim();
        }

        #endregion

        #region Поля и свойства

        public string FirstName { get; }

        public string FullName => $"{LastName} {FirstName} {MidName}".Replace("  ", string.Empty);

        public string LastName { get; }

        public string MidName { get; }

        #endregion
    }
}