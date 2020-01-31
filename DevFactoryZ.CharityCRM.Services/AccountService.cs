using DevFactoryZ.CharityCRM.Persistence;
using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.Services
{
    /// <summary>
    /// Имплементация <see cref="IAccountService"/> для работы с хранилищем <see cref="Account"/>'s.
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository repository;

        /// <summary>
        /// Создает экземпляр <see cref="AccountService"/>.
        /// </summary>
        /// <param name="repository">Имплементация <see cref="IAccountRepository"/>, используемая сервисом <see cref="AccountService"/> для работы с хранилищем.</param>
        public AccountService(IAccountRepository repository)
        {
            this.repository = repository;
        }

        public Account Create(Account newAccount)
        {
            repository.Create(newAccount);
            repository.Save();

            return newAccount;
        }

        public void Delete(int id)
        {
            repository.Delete(id);
            repository.Save();
        }

        public IEnumerable<Account> GetAll()
        {
            return repository.GetAll();
        }

        public Account GetById(int id)
        {
            return repository.GetById(id);
        }

        public Account GetByLogin(string login)
        {
            return repository.GetByLogin(login);
        }

        public void Update(string login, char[] newPassword)
        {
            var account = GetByLogin(login);
            account.Password.ChangeTo(newPassword);
            
            repository.Save();
        }
    }
}
