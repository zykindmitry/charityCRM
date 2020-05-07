using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore
{
    internal class WardRepository : IWardRepository
    {
        private readonly DbSet<Ward> setOfWards;

        private readonly Action save;

        public WardRepository(DbSet<Ward> setOfWards, Action save)
        {
            this.setOfWards = setOfWards;
            this.save = save;
        }

        public void Add(Ward role)
        {
            setOfWards.Add(role);
        }

        public void Delete(int id)
        {
            setOfWards.Remove(GetById(id));
        }

        public IEnumerable<Ward> GetAll()
        {
            return setOfWards.ToArray();
        }

        public Ward GetById(int id)
        {
            return setOfWards
                .Include(r => r.Address)
                .Include(r => r.WardCategories)
                    .ThenInclude(x => x.WardCategory)                
                .FirstOrDefault(r => r.Id == id)
                ?? throw new EntityNotFoundException(id, typeof(Ward));
        }

        public void Save()
        {
            save();
        }
    }
}
