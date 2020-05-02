using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore
{
    internal class WardCategoryRepository : IWardCategoryRepository
    {
        private readonly DbSet<WardCategory> setOfWardCategories;

        private readonly Action save;

        public WardCategoryRepository(DbSet<WardCategory> setOfWardCategories, Action save)
        {
            this.setOfWardCategories = setOfWardCategories;
            this.save = save;
        }

        public void Add(WardCategory permission)
        {
            setOfWardCategories.Add(permission);
        }

        public void Delete(int id)
        {
            setOfWardCategories.Remove(GetById(id));
        }

        public IEnumerable<WardCategory> GetAll()
        {
            return setOfWardCategories
                .Include(c => c.SubCategories).ToArray();
        }

        public WardCategory GetById(int id)
        {
            return setOfWardCategories
                .Include(r => r.SubCategories)
                .FirstOrDefault(r => r.Id == id)
                ?? throw new EntityNotFoundException(id, typeof(WardCategory));
        }

        public void Save()
        {
            save();
        }
    }
}
