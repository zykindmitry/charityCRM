using DevFactoryZ.CharityCRM.Persistence;
using System.Collections.Generic;
using System;

namespace DevFactoryZ.CharityCRM.Services
{

    /// <summary>
    /// Имплементация <see cref="IAccountSessionService"/> для работы с хранилищем <see cref="AccountSession"/>'s.
    /// </summary>
    public class AccountSessionService : IAccountSessionService
    {
        private readonly IAccountSessionRepository repository;

        /// <summary>
        /// Создает экземпляр <see cref="AccountSessionService"/>.
        /// </summary>
        /// <param name="repository">Имплементация <see cref="IAccountSessionRepository"/>, 
        /// используемая сервисом <see cref="AccountSessionService"/> для работы с хранилищем.</param>
        public AccountSessionService(IAccountSessionRepository repository)
        {
            this.repository = repository;
        }

        public AccountSession Create(AccountSession newAccountSession)
        {
            repository.Create(newAccountSession);
            repository.Save();

            return newAccountSession;
        }

        public void Delete(Guid id)
        {
            repository.Delete(id);
            repository.Save();
        }

        public IEnumerable<AccountSession> GetAll()
        {
            return repository.GetAll();
        }

        public AccountSession GetById(Guid id)
        {
            return repository.GetById(id);
        }
    }
}
