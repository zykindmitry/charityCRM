using System;

namespace DevFactoryZ.CharityCRM
{

    /// <summary>
    /// Представляет фамилию, имя, отчество подопечного БФ.
    /// </summary>
    public class FullName : IFullName
    {
        #region .ctor
        
        /// <summary>
        /// Для создания миграций
        /// </summary>        
        public FullName()
        {

        }        

        /// <summary>
        /// Создает экземпляр <see cref="CharityCRM.FullName"/>
        /// </summary>
        /// <param name="surName">Фамилия</param>
        /// <param name="firstName">Имя</param>
        /// <param name="middleName">Отчество</param>
        public FullName(string surName, string firstName, string middleName) : this()
        {
            this.firstName.Value = firstName.Trim();
            FirstName = this.firstName.Value;

            this.middleName.Value = middleName.Trim();
            MiddleName = this.middleName.Value;

            this.surName.Value = surName.Trim();
            SurName = this.surName.Value;
        }

        #endregion

        #region Поля и свойства

        public static bool FirstNameIsRequired = true;

        public static int FirstNameMaxLength = 30;

        private readonly RealString firstName =
            new RealString(FirstNameMaxLength, FirstNameIsRequired, "имя подопечного");

        public string FirstName { get; private set; }

        public static bool SurNameIsRequired = true;

        public static int SurNameMaxLength = 30;

        private readonly RealString surName =
            new RealString(SurNameMaxLength, SurNameIsRequired, "фамилия подопечного");

        public string SurName { get; private set; }

        public static bool MiddleNameIsRequired = false;

        public static int MiddleNameMaxLength = 30;

        private readonly RealString middleName =
            new RealString(MiddleNameMaxLength, MiddleNameIsRequired, "отчество подопечного");

        public string MiddleName { get; private set; }

        #endregion

        #region Методы

        public void Update(IFullName newFullName)
        {
            if (newFullName == null)
            {
                throw new ArgumentNullException(nameof(newFullName));
            }

            FirstName = newFullName.FirstName;
            MiddleName = newFullName.MiddleName;
            SurName = newFullName.SurName;        
        }

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
            return base.GetHashCode();
        }

        public static bool operator ==(FullName left, FullName right) => left.Equals(right);
        public static bool operator !=(FullName left, FullName right) => !(left == right);

        #endregion
    }
}