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

        public void Add(WardCategory wardCategory)
        {
            setOfWardCategories.Add(wardCategory);
        }

        public void Delete(int id)
        {
            setOfWardCategories.Remove(GetById(id));
        }

        public IEnumerable<WardCategory> GetAll()
        {
            return setOfWardCategories.Include(r => r.SubCategories).ToArray();
        }

        public WardCategory GetById(int id)
        {
            return setOfWardCategories
                .Include(r => r.SubCategories)
                .FirstOrDefault(r => r.Id == id)
                ?? throw new EntityNotFoundException(id, typeof(WardCategory));
        }

        public IEnumerable<WardCategory> GetRoots()
        {
            return setOfWardCategories
                .Include(r => r.SubCategories)
                .Where(r => EF.Property<int?>(r, "ParentId") == null);
        }

        public bool HasCycling(int parentId, int childId, bool isCycling = false)
        {
            if (isCycling)
            {
                return isCycling;
            }

            var child = GetById(childId);

            var children = child.SubCategories;

            if (children.Any(s => s.Id == parentId))
            {
                isCycling = true;
                return isCycling;
            }

            foreach (var childItem in children)
            {
                isCycling = HasCycling(parentId, childItem.Id, isCycling);

                if (isCycling)
                {
                    break;
                }
            }

            return isCycling;
        }

        public bool HasParent(int wardCategoryId)
        {
            return setOfWardCategories.Any( wc => 
                wc.Id == wardCategoryId && EF.Property<int?>(wc, "ParentId") != null);
        }

        public void Save()
        {
            save();
        }
    }
}
