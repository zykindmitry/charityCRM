using DevFactoryZ.CharityCRM.Persistence;
using System.Collections.Generic;
using System.Text;

namespace DevFactoryZ.CharityCRM.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository repository;

        public AccountService(IAccountRepository repository)
        {
            this.repository = repository;
        }
        public Account Create(AccountData data)
        {
            var account = new Account(data.Login, data.PasswordConfig);
            repository.Create(account);
            repository.Save();

            return account;
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

        public void Update(int id, AccountData data)
        {
            var account = repository.GetById(id);
            account = new Account(data.Login, data.PasswordClearText, data.PasswordConfig);

            repository.Save();

        }
    }
}
