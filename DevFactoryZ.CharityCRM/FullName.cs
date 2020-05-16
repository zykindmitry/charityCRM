namespace DevFactoryZ.CharityCRM
{

    /// <summary>
    /// Представляет фамилию, имя, отчество.
    /// </summary>
    public class FullName
    {
        #region .ctor
        
        /// <summary>
        /// Для создания миграций
        /// </summary>        
        protected FullName()
        {

        }

        /// <summary>
        /// Создает экземпляр <see cref="FullName"/>
        /// </summary>
        /// <param name="surName">Фамилия</param>
        /// <param name="firstName">Имя</param>
        /// <param name="middleName">Отчество</param>
        public FullName(string surName, string firstName, string middleName) : this()
        {
            this.firstName.Value = firstName?.Trim() ?? string.Empty;

            this.middleName.Value = middleName?.Trim() ?? string.Empty;

            this.surName.Value = surName?.Trim() ?? string.Empty;
        }

        #endregion

        #region Поля и свойства

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get => firstName.Value; set => firstName.Value = value; }

        public static bool FirstNameIsRequired = true;

        public static int FirstNameMaxLength = 30;

        private readonly RealString firstName =
            new RealString(FirstNameMaxLength, FirstNameIsRequired, "имя");

        /// <summary>
        /// Фамилия
        /// </summary>
        public string SurName { get => surName.Value; set => surName.Value = value; }

        public static bool SurNameIsRequired = true;

        public static int SurNameMaxLength = 30;

        private readonly RealString surName =
            new RealString(SurNameMaxLength, SurNameIsRequired, "фамилия");

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get => middleName.Value; set => middleName.Value = value; }

        public static bool MiddleNameIsRequired = false;

        public static int MiddleNameMaxLength = 30;

        private readonly RealString middleName =
            new RealString(MiddleNameMaxLength, MiddleNameIsRequired, "отчество");

        #endregion

        #region Переопределенные методы

        public override string ToString()
        {
            return $"{SurName} {FirstName} {MiddleName}".Replace("  ", string.Empty);
        }

        public override bool Equals(object obj)
        {
            return obj is FullName compareWith && ToString() == compareWith.ToString();
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public static bool operator ==(FullName left, FullName right) => left.Equals(right);
        public static bool operator !=(FullName left, FullName right) => !(left == right);

        #endregion
    }
}