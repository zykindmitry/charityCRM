using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore
{
    internal class AccountSessionRepository : IAccountSessionRepository
    {
        private readonly DbSet<AccountSession> setOfAccountSessions;

        private readonly Action save;

        public AccountSessionRepository(DbSet<AccountSession> setOfAccountSessions, Action save)
        {
            this.setOfAccountSessions = setOfAccountSessions;
            this.save = save;
        }

        public void Create(AccountSession repositoryType)
        {
            setOfAccountSessions.Add(repositoryType);
        }

        public void Delete(Guid id)
        {
            setOfAccountSessions.Remove(GetById(id));
        }

        public IEnumerable<AccountSession> GetAll()
        {
            return setOfAccountSessions
                .Include(a => a.Account)
                .ToArray();
        }

        public AccountSession GetById(Guid id)
        {
            return setOfAccountSessions
                .Include(a => a.Account)
                .FirstOrDefault(a => a.Id == id)
                ?? throw new EntityNotFoundException(id, typeof(Account));
        }

        public void Save()
        {
            save();
        }

        public AccountSession Update(AccountSession repositoryType)
        {
            return setOfAccountSessions.Update(repositoryType).Entity;
        }
    }
}
