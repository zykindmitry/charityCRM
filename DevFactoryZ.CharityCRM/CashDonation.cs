using System;
using System.Collections.Generic;
using System.Text;
namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Представляет денежное пожертвование в пользу одного из подопечных благотворительного фонда.
    /// </summary>
    public class CashDonation : Donation
    {
        #region .ctor

        protected CashDonation() // для ORM
        {
        }

        /// <summary>
        /// Создает экземпляр типа DevFactoryZ.CharityCRM.CashDonation.
        /// </summary>
        /// <param name="amount">Сумма пожертвования (в рублях).</param>
        public CashDonation(double amount, string description = null)
            : this()
        {
            Amount = amount;
            Description = description;
        }

        #endregion

        #region Хранение и идентификация

        public override bool Equals(object obj)
        {
            return (obj is CashDonation cashDonation) 
                && (cashDonation.Id == Id && cashDonation.Amount == Amount);
        }

        public override int GetHashCode()
        {
            int firstLittlePrimeNumber = 19;
            int secondLittlePrimeNumber = 37;
            
            var hash = firstLittlePrimeNumber;
            hash = hash * secondLittlePrimeNumber + Id.GetHashCode();
            hash = hash * secondLittlePrimeNumber + Amount.GetHashCode();

            return hash;
        }

        #endregion

        #region Описание денежного пожертвования

        /// <summary>
        /// Возвращает сумму пожертвования (в рублях)
        /// </summary>
        public double Amount { get; }

        #endregion
    }
}
