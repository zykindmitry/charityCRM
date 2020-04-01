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
            if (!(setOfDonations is DbSet<Donation>))
            {
                throw new ArgumentException
                    ($"Для инициаизации экземпляра '{typeof(DonationRepository<TEntity>).Name}' следует использовать тип '{typeof(Donation).Name}'.", nameof(setOfDonations));
            }

            this.setOfDonations = setOfDonations;
            this.save = save;
        }

        public void Add(Donation entity)
        {
            setOfDonations.Add(entity as TEntity
                ?? throw new ArgumentNullException(nameof(entity), $"Не задана сущность типа '{typeof(TEntity).Name}' для добавления в хранилище"));
        }

        public void Delete(long id)
        {
            setOfDonations.Remove(GetById(id) as TEntity);
        }

        public Donation GetById(long id)
        {
            var res = setOfDonations.Find(id)
                ?? throw new EntityNotFoundException(id, typeof(TEntity));

            if (res.GetType() == typeof(CommodityDonation))
            {
                var dbset = (new CharityDbContext()).Set<CommodityDonation>();

                return dbset.Include(p => p.Commodities).FirstOrDefault(p => p.Id == id) as Donation;
            }

            return res as Donation; 
        }

        public IEnumerable<Donation> GetAll()
        {
            return setOfDonations.ToArray() as IEnumerable<Donation>;
        }

        public void Save()
        {
            save();
        }

        public IEnumerable<Entity> GetAll<Entity>()
        {
            IEnumerable<Entity> result;

            if (typeof(Entity) == typeof(CommodityDonation))
            {
                var dbset = (new CharityDbContext()).Set<CommodityDonation>();
                result = dbset.Include(p => p.Commodities).ToArray() as IEnumerable<Entity>;
            }
            else if (typeof(Entity) == typeof(CashDonation))
            {
                var dbset = (new CharityDbContext()).Set<CashDonation>();
                result = dbset.ToArray() as IEnumerable<Entity>;
            }
            else
            {
                throw new ArgumentException($"Тип '{typeof(Entity).Name}' не поддерживается в текущей реализации.");
            }
            return result;
        }

    }
}
