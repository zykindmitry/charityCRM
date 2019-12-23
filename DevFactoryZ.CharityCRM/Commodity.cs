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
        /// <param name="commodityDonation">Пожертвование, в рамках которого передается предмет (группа предметов).</param>
        /// <param name="description">Описание пожертвованного предмета (группы предметов).</param>
        /// <param name="quantity">Количество пожертвованных предметов.</param>
        /// <param name="cost">Стоимость пожертвованных предметов (в рублях, необязательное поле).</param>
        public Commodity(CommodityDonation commodityDonation,  string description, int quantity, decimal? cost = null)
            : this()
        {
            CommodityDonation = commodityDonation ??
                throw new ArgumentNullException(nameof(commodityDonation), "Не указано пожертвование, в рамках которого передается предмет (группа предметов).");
            
            Quantity = quantity > 0 ? quantity
                : throw new ArgumentException("Количество передаваемых предметова должно быть больше 0.", nameof(quantity));

            Description = description;
            Cost = cost;
        }

        #endregion

        #region Хранение и идентификация

        /// <summary>
        /// Возвращает идентификатор предмета (группы предметов), генерируемый на стороне хранилища.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// Возвращает пожертвование, в рамках которого передается предмет (группа предметов).
        /// </summary>
        public CommodityDonation CommodityDonation { get; }

        /// <summary>
        /// Возвращает признак доступности удаления предмета (группы предметов) из хранилища данных системы.
        /// </summary>
        public bool CanBeDeleted => true;

        public override bool Equals(object obj)
        {
            return (obj is Commodity commodity) 
                && commodity.Id == Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        #region Описание, количество, стоимость предмета (группы предметов)

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
        public int Quantity { get; }

        /// <summary>
        /// Возвращает стоимость пожертвованных предметов (в рублях, необязательное поле).
        /// </summary>
        public decimal? Cost { get; }

        public override string ToString()
        {
            return Description;
        }

        #endregion
    }
}
