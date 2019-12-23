using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Представляет пожертвование в виде предметов 
    /// (одежда, продукты питания, книги и т.п.).
    /// </summary>
    public class CommodityDonation : Donation
    {
        #region .ctor

        protected CommodityDonation() // для ORM
            : base()
        {
        }

        /// <summary>
        /// Создает экземпляр типа DevFactoryZ.CharityCRM.CommodityDonation.
        /// </summary>
        /// <param name="commodities">Перечень пожертвованных предметов (групп предметов).</param>
        /// <param name="description">Описание пожертвования.</param>
        public CommodityDonation(IEnumerable<Commodity> commodities, string description)
            : base(description)
        {
            commodities.Each(commodity => this.commodities.Add(commodity));
        }

        #endregion

        #region Хранение и идентификация

        // Т.к. CommodityDonation и CashDonation хранятся в одной таблице, то не перегружаем Equals и GetHashCode

        #endregion

        #region Перечень предметов, переданных в качестве пожертвования

        private readonly List<Commodity> commodities = new List<Commodity>();

        /// <summary>
        /// Возвращает перечень пожертвованных предметов (групп предметов).
        /// </summary>
        public IEnumerable<Commodity> Commodities => commodities.AsReadOnly();

        public override string ToString()
        {
            return Description;
        }

        #endregion
    }
}
