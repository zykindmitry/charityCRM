using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore
{
    internal class DonationRepository<TEntity> : IDonationRepository
        where TEntity : class, IAmPersistent<long>
    {
        private readonly DbSet<TEntity> setOfDonations;

        private readonly Action save;

        public DonationRepository(DbSet<TEntity> setOfDonations, Action save)
        {
            if (!(setOfDonations is DbSet<CashDonation>) && !(setOfDonations is DbSet<CommodityDonation>))
            {
                throw new ArgumentException($"Тип '{typeof(TEntity).Name}' не поддерживается в текущей реализации.", nameof(setOfDonations));
            }

            this.setOfDonations = setOfDonations;
            this.save = save;
        }

        #region Явная реализация ICashDonation

        void IRepository<CashDonation, long>.Create(CashDonation repositoryType)
        {
            if (repositoryType.Amount == 0)
            {
                throw new ArgumentException("Денежное пожертвование равно 0. Так не бывает", nameof(repositoryType.Amount));
            }

            Create(repositoryType as TEntity);
        }

        void IRepository<CashDonation, long>.Delete(long id)
        {
           Remove(id);
        }

        CashDonation IRepository<CashDonation, long>.GetById(long id)
        {
            return (setOfDonations.Find(id) as CashDonation)
                ?? throw new EntityNotFoundException(id, typeof(TEntity));
        }

        IEnumerable<CashDonation> IRepository<CashDonation, long>.GetAll()
        {
            return setOfDonations.ToArray() as IEnumerable<CashDonation>;
        }

        void IRepository<CashDonation, long>.Save()
        {
            save();
        }

        #endregion

        #region Явная реализация ICommodityDonation

        void IRepository<CommodityDonation, long>.Create(CommodityDonation repositoryType)
        {
            if (repositoryType.Commodities.Count() == 0)
            {
                throw new ArgumentException("Пожертвование предметами (вещами) не содержит ни одного предмета (вещи).", nameof(repositoryType.Commodities));
            }

            Create(repositoryType as TEntity);
        }

        void IRepository<CommodityDonation, long>.Delete(long id)
        {
            Remove(id);
        }
        CommodityDonation IRepository<CommodityDonation, long>.GetById(long id)
        {
            return setOfDonations.Include(p => (p as CommodityDonation).Commodities)
                .FirstOrDefault(p => p.Id == id) as CommodityDonation
                ?? throw new EntityNotFoundException(id, typeof(TEntity));
        }

        IEnumerable<CommodityDonation> IRepository<CommodityDonation, long>.GetAll()
        {
            return setOfDonations.Include(p => (p as CommodityDonation).Commodities)
                .ToArray() as IEnumerable<CommodityDonation>;
        }

        void IRepository<CommodityDonation, long>.Save()
        {
            save();
        }

        #endregion

        #region Обобщенные методы


        void Create(TEntity entity)
        {
            setOfDonations.Add(entity
                ?? throw new ArgumentNullException(nameof(entity), $"Не задана сущность типа '{typeof(TEntity).Name}' для добавления в хранилище"));
        }

        void Remove(long id)
        {
            setOfDonations.Remove(GetById(id));
        }

        TEntity GetById(long id)
        {
            return setOfDonations.Find(id)
                ?? throw new EntityNotFoundException(id, typeof(TEntity)); 
        }

        #endregion
    }
}
