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
        {
        }

        /// <summary>
        /// Создает экземпляр типа DevFactoryZ.CharityCRM.CommodityDonation.
        /// </summary>
        /// <param name="commodities">Перечень пожертвованных предметов (групп предметов).</param>
        public CommodityDonation(IEnumerable<Commodity> commodities)
            : this()
        {
            commodities.Each(commodity => this.commodities.Add(commodity));
        }

        #endregion

        #region Хранение и идентификация

        public override bool Equals(object obj)
        {
            return (obj is CommodityDonation commodityDonation) && commodityDonation.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion

        #region Описание пожертвования в виде предметов

        public static string CommoditiesFieldName => nameof(commodities);

        private readonly List<Commodity> commodities = new List<Commodity>();

        /// <summary>
        /// Возвращает перечень пожертвованных предметов (групп предметов).
        /// </summary>
        public IEnumerable<Commodity> Commodities => commodities.AsReadOnly();

        #endregion
    }
}
