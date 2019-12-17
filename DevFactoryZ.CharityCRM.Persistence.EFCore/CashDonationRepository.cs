using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore
{
    internal class CashDonationRepository : ICashDonationRepository
    {
        private readonly DbSet<CashDonation> dbSet;

        private readonly Action save;

        public CashDonationRepository(DbSet<CashDonation> dbSet, Action save)
        {
            this.dbSet = dbSet;
            this.save = save;
        }

        public void Create(CashDonation cashDonation)
        {
            dbSet.Add(cashDonation);
        }

        public void Delete(int id)
        {
            dbSet.Remove(GetById(id));
        }

        public IEnumerable<CashDonation> GetAll()
        {
            return dbSet.ToArray();
        }

        public CashDonation GetById(int id)
        {
            return dbSet
                .Find(id)
                ?? throw new EntityNotFoundException(id, typeof(CashDonation));
        }

        public void Save()
        {
            save();
        }
    }
}
