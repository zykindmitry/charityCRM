namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Представляет почтовый адрес
    /// </summary>
    public class Address
    {
        #region .ctor

        /// <summary>
        /// Для создания миграций
        /// </summary>        
        protected Address()
        {

        }

        /// <summary>
        /// Создает экземпляр <see cref="Address"/>
        /// </summary>
        /// <param name="postCode">Почтовый индекс</param>
        /// <param name="country">Страна</param>
        /// <param name="region">Область, край и т.п.</param>
        /// <param name="city">Населенный пункт</param>
        /// <param name="area">Округ, район и т.п.</param>
        /// <param name="street">Улица</param>
        /// <param name="house">Дом</param>
        /// <param name="flat">Квартира</param>
        public Address(
            string postCode,
            string country,
            string region,
            string city,
            string area,
            string street,
            string house,
            string flat)
            : this()
        {
            this.postCode.Value = postCode?.Trim() ?? string.Empty;
            this.country.Value = country?.Trim() ?? string.Empty;
            this.region.Value = region?.Trim() ?? string.Empty;
            this.city.Value = city?.Trim() ?? string.Empty;
            this.area.Value = area?.Trim() ?? string.Empty;
            this.street.Value = street?.Trim() ?? string.Empty;
            this.house.Value = house?.Trim() ?? string.Empty;
            this.flat.Value = flat?.Trim() ?? string.Empty;
        }

        #endregion

        #region Поля и свойства

        /// <summary>
        /// Почтовый индекс
        /// </summary>
        public string PostCode { get => postCode.Value; set => postCode.Value = value; }

        public static bool PostCodeIsRequired = false;

        public static int PostCodeMaxLength = 6;

        private readonly RealString postCode =
            new RealString(PostCodeMaxLength, PostCodeIsRequired, "почтовый индекс");

        /// <summary>
        /// Страна
        /// </summary>
        public string Country { get => country.Value; set => country.Value = value; }

        public static bool CountryIsRequired = false;

        public static int CountryMaxLength = 50;

        private readonly RealString country =
            new RealString(CountryMaxLength, CountryIsRequired, "страна");
        
        /// <summary>
        /// Область, край и т.п.
        /// </summary>
        public string Region { get => region.Value; set => region.Value = value; }

        public static bool RegionIsRequired = false;

        public static int RegionMaxLength = 50;

        private readonly RealString region =
            new RealString(RegionMaxLength, RegionIsRequired, "область, край и т.п.");

        /// <summary>
        /// Населенный пункт
        /// </summary>
        public string City { get => city.Value; set => city.Value = value; }

        public static bool CityIsRequired = true;

        public static int CityMaxLength = 50;

        private readonly RealString city =
            new RealString(CityMaxLength, CityIsRequired, "город, поселок, село и т.п.");

        /// <summary>
        /// Район, округ и т.п.
        /// </summary>
        public string Area { get => area.Value; set => area.Value = value; }

        public static bool AreaIsRequired = false;

        public static int AreaMaxLength = 50;

        private readonly RealString area =
            new RealString(AreaMaxLength, AreaIsRequired, "район, округ и т.п.");

        /// <summary>
        /// Улица
        /// </summary>
        public string Street { get => street.Value; set => street.Value = value; }

        public static bool StreetIsRequired = false;

        public static int StreetMaxLength = 50;

        private readonly RealString street =
            new RealString(StreetMaxLength, StreetIsRequired, "улица, проспект, переулок и т.п.");

        /// <summary>
        /// Дом, строение, корпус и т.п. с номером
        /// </summary>
        public string House { get => house.Value; set => house.Value = value; }

        public static bool HouseIsRequired = false;

        public static int HouseMaxLength = 30;

        private readonly RealString house =
            new RealString(HouseMaxLength, HouseIsRequired, "номер дома, строения, корпуса и т.п.");

        /// <summary>
        /// Номер квартиры
        /// </summary>
        public string Flat { get => flat.Value; set => flat.Value = value; }

        public static bool FlatIsRequired = false;

        public static int FlatMaxLength = 10;

        private readonly RealString flat =
            new RealString(FlatMaxLength, FlatIsRequired, "номер квартиры");

        #endregion

        #region Переопределенные методы

        public override string ToString()
        {
            return $"{PostCode}, {Country}, {Region}, {City}, {Area}, {Street}, {House}, {Flat}";
        }

        public override bool Equals(object obj)
        {
            return obj is Address compareWith && ToString() == compareWith.ToString();
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public static bool operator ==(Address left, Address right) => left.Equals(right);
        public static bool operator !=(Address left, Address right) => !(left == right);

        #endregion
    }
}
