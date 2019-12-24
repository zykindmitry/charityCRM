using System.Collections.Generic;
using System;

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
        /// <param name="description">Описание пожертвования.</param>
        public CommodityDonation(string description)
            : base(description)
        {
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

        /// <summary>
        /// Добавление предмета в перечень предметов для текущего пожертвования.
        /// </summary>
        /// <param name="commodity">Пожертвованный предмет (группа предметов).</param>
        public void AddCommodity(Commodity commodity)
        {
            commodities.Add(commodity 
                ?? throw new ArgumentNullException(nameof(commodity), $"Экземпляр '{nameof(Commodity)}' для добавления не может быть 'NULL'."));
        }

        /// <summary>
        /// Добавление коллекции предметов в перечень предметов для текущего пожертвования.
        /// </summary>
        /// <param name="commodities">Перечень пожертвованных предметов (групп предметов).</param>
        public void AddCommodities(IEnumerable<Commodity> commodities)
        {
            commodities.Each(commodity => AddCommodity(commodity));
        }

        /// <summary>
        /// Удаление предмета из перечня предметов для текущего пожертвования.
        /// </summary>
        /// <param name="commodity">Пожертвованный предмет (группа предметов).</param>
        public void RemoveCommodity(Commodity commodity)
        {
            commodities.Remove(commodity
                ?? throw new ArgumentNullException(nameof(commodity), $"Экземпляр '{nameof(Commodity)}' для удаления не может быть 'NULL'."));
        }

        /// <summary>
        /// Удаление нескольких предметов из перечня предметов для текущего пожертвования.
        /// </summary>
        /// <param name="commodities">Перечень пожертвованных предметов (групп предметов).</param>
        public void RemoveCommodities(IEnumerable<Commodity> commodities)
        {
            commodities.Each(commodity => RemoveCommodity(commodity));
        }

        public override string ToString()
        {
            return Description;
        }

        #endregion
    }
}
