using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore
{
    internal class AccountRepository : IAccountRepository
    {
        private readonly DbSet<Account> setOfAccounts;

        private readonly Action save;

        public AccountRepository(DbSet<Account> setOfAccounts, Action save)
        {
            this.setOfAccounts = setOfAccounts;
            this.save = save;
        }

        public void Create(Account repositoryType)
        {
            setOfAccounts.Add(repositoryType);
        }

        public void Delete(int id)
        {
            setOfAccounts.Remove(GetById(id));
        }

        public IEnumerable<Account> GetAll()
        {
            return setOfAccounts.ToArray();
        }

        public Account GetById(int id)
        {
            return setOfAccounts
                .Include(a => a.Password)
                    .ThenInclude(p => p.PasswordConfig)
                .FirstOrDefault(a => a.Id == id)
                ?? throw new EntityNotFoundException(id, typeof(Account));
        }

        public Account GetByLogin(string login)
        {
            return setOfAccounts
                .Include(a => a.Password)
                    .ThenInclude(p => p.PasswordConfig)
                .FirstOrDefault(a => a.Login == login)
                ?? throw new EntityNotFoundException(login, typeof(Account)); ;
        }

        public void Save()
        {
            save();
        }
    }
}
