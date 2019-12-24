namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Представляет денежное пожертвование в пользу одного из подопечных благотворительного фонда.
    /// </summary>
    public class CashDonation : Donation
    {
        #region .ctor

        protected CashDonation() // для ORM
            : base()
        {
        }

        /// <summary>
        /// Создает экземпляр типа DevFactoryZ.CharityCRM.CashDonation.
        /// </summary>
        /// <param name="amount">Сумма пожертвования (в рублях).</param>
        /// <param name="description">Описание пожертвования.</param>
        public CashDonation(decimal amount, string description)
            : base(description)
        {
            Amount = amount;
        }

        #endregion

        #region Хранение и идентификация

        // Т.к. CommodityDonation и CashDonation хранятся в одной таблице, то не перегружаем Equals и GetHashCode

        #endregion

        #region Сумма денежного пожертвования

        /// <summary>
        /// Возвращает сумму пожертвования (в рублях)
        /// </summary>
        public decimal Amount { get; private set; } // Определяем сеттер, чтобы свойство Amount попало в таблицу Donations, 
                                                    //созданную по шаблону Table Per Hierarchy с помощью IEntityTypeConfiguration

        public override string ToString()
        {
            return Description;
        }

        #endregion
    }
}
