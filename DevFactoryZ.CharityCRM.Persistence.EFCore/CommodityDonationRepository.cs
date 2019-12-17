using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore
{
    internal class CommodityDonationRepository : ICommodityDonationRepository
    {
        private readonly DbSet<CommodityDonation> dbSet;

        private readonly Action save;

        public CommodityDonationRepository(DbSet<CommodityDonation> dbSet, Action save)
        {
            this.dbSet = dbSet;
            this.save = save;
        }

        public void Create(CommodityDonation commodityDonation)
        {
            dbSet.Add(commodityDonation);
        }

        public void Delete(int id)
        {
            dbSet.Remove(GetById(id));
        }

        public IEnumerable<CommodityDonation> GetAll()
        {
            return dbSet.ToArray();
        }

        public CommodityDonation GetById(int id)
        {
            return dbSet
                .Include(s => s.Commodities)
                .FirstOrDefault(s => s.Id == id)
                ?? throw new EntityNotFoundException(id, typeof(CommodityDonation));
        }

        public void Save()
        {
            save();
        }
    }
}
