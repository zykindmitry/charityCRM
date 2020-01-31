using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore
{
    public class PasswordConfigRepository : IPasswordConfigRepository
    {

        private readonly DbSet<PasswordConfig> setOfPasswordConfigs;

        private readonly Action save;

        public PasswordConfigRepository(DbSet<PasswordConfig> setOfPasswordConfigs, Action save)
        {
            this.setOfPasswordConfigs = setOfPasswordConfigs;
            this.save = save;
        }

        public void Create(PasswordConfig repositoryType)
        {
            setOfPasswordConfigs.Add(repositoryType);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException("Удаление конфигураций сложности пароля запрещено.");
        }

        public IEnumerable<PasswordConfig> GetAll()
        {
            return setOfPasswordConfigs.ToArray();
        }

        public PasswordConfig GetById(int id)
        {
            return setOfPasswordConfigs
                .FirstOrDefault(p => p.Id == id)
                ?? throw new EntityNotFoundException(id, typeof(PasswordConfig));
        }

        public IPasswordConfig GetCurrent()
        {
            return setOfPasswordConfigs
                .OrderByDescending(g => g.CreatedAt)                
                .FirstOrDefault() 
                ?? throw new EntityNotFoundException(null, typeof(PasswordConfig));
        }

        public void Save()
        {
            save();
        }
    }
}
