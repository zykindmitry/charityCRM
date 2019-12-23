using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore
{
    internal class DonationRepository<TEntity, TKey> : IDonationRepository<TEntity, TKey> 
        where TEntity : class, IAmPersistent<TKey>
        where TKey : struct
    {
        private readonly DbSet<TEntity> setOfDonations;

        private readonly Action save;

        public DonationRepository(DbSet<TEntity> setOfDonations, Action save)
        {
            if (!(setOfDonations is CashDonation) && !(setOfDonations is CommodityDonation))
            {
                throw new ArgumentException($"Тип '{typeof(TEntity).Name}' не поддерживается в текущей реализации.", nameof(setOfDonations));
            }

            this.setOfDonations = setOfDonations;
            this.save = save;
        }

        public void Create(TEntity entity)
        {
            setOfDonations.Add(entity 
                ?? throw new ArgumentNullException(nameof(entity), $"Не задана сущность типа '{typeof(TEntity).Name}' для добавления в хранилище"));
        }

        public void Delete(TKey id)
        {
            setOfDonations.Remove(GetById(id));
        }

        public IEnumerable<TEntity> GetAll()
        {
            return setOfDonations.ToListAsync().Result;
        }

        public TEntity GetById(TKey id)
        {
            return setOfDonations.Find(id)
                ?? throw new EntityNotFoundException(id, typeof(TEntity));
        }

        public void Save()
        {
            save();
        }
    }
}
