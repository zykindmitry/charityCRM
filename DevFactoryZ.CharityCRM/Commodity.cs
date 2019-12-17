using System;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Представляет предметы (одежда, продукты питания, книги и т.п.), 
    /// переданные подопечному фонда в качестве пожертвования
    /// </summary>
    public class Commodity : IAmPersistent<int>
    {
        #region .ctor

        protected Commodity() // для ORM
        {
        }

        /// <summary>
        /// Создает экземпляр типа DevFactoryZ.CharityCRM.Commodity.
        /// </summary>
        /// <param name="description">Описание пожертвованного предмета (группы предметов).</param>
        /// <param name="quantity">Количество пожертвованных предметов.</param>
        /// <param name="cost">Стоимость пожертвованных предметов (в рублях, необязательное поле).</param>
        public Commodity(string description, uint quantity, Nullable<double> cost = null)
            : this()
        {
            Description = description;
            Quantity = quantity;
            Cost = cost;
        }

        #endregion

        #region Хранение и идентификация

        /// <summary>
        /// Возвращает идентификатор предмета (группы предметов), генерируемый на стороне хранилища.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// Внешний ключ 
        /// </summary>
        public int CommodityDonationId { get; set; }

        /// <summary>
        /// Навигационное свойство
        /// </summary>
        public CommodityDonation CommodityDonation { get; set; }

        /// <summary>
        /// Возвращает признак доступности удаления предмета (группы предметов) из хранилища данных системы.
        /// </summary>
        public bool CanBeDeleted => true;

        public override bool Equals(object obj)
        {
            return (obj is Commodity commodity) 
                && (commodity.Id == Id && commodity.Quantity == Quantity && commodity.Cost == Cost);
        }

        public override int GetHashCode()
        {
            int firstLittlePrimeNumber = 19;
            int secondLittlePrimeNumber = 37;

            var hash = firstLittlePrimeNumber;
            hash = hash * secondLittlePrimeNumber + Id.GetHashCode();
            hash = hash * secondLittlePrimeNumber + Quantity.GetHashCode();
            hash = hash * secondLittlePrimeNumber + Cost.GetHashCode();

            return hash;
        }

        #endregion

        #region Описание предмета (группы предметов)

        public static bool DescriptionIsRequired = true;

        public static int DescriptionMaxLength = 500;

        private readonly RealString description =
            new RealString(DescriptionMaxLength, DescriptionIsRequired, "описание пожертвованного предмета (группы предметов)");

        /// <summary>
        /// Возвращает или задает описание пожертвованного предмета (группы предметов).
        /// </summary>
        public string Description { get => description.Value; set => description.Value = value; }

        /// <summary>
        /// Возвращает количество пожертвованных предметов.
        /// </summary>
        public uint Quantity { get; }

        /// <summary>
        /// Возвращает стоимость пожертвованных предметов (в рублях, необязательное поле).
        /// </summary>
        public Nullable<double> Cost { get; }

        public override string ToString()
        {
            return Description;
        }

        #endregion
    }
}
