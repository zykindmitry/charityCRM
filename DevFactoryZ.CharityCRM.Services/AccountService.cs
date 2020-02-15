using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.Services
{
    /// <summary>
    /// Имплементация <see cref="IAccountService"/> для работы с хранилищем <see cref="Account"/>'s.
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository repository;
        private readonly IAccountSessionService accountSessionService;
        private readonly IAccountSessionConfig accountSessionConfig;

        /// <summary>
        /// Создает экземпляр <see cref="AccountService"/>.
        /// </summary>
        /// <param name="repository">Имплементация <see cref="IAccountRepository"/>, используемая сервисом <see cref="AccountService"/> для работы с хранилищем.</param>
        /// <param name="accountSessionService">Имплементация <see cref="IAccountSessionService"/>, 
        /// используемая сервисом <see cref="AccountService"/> для работы с хранилищем пользовательских сессий.</param>
        /// <param name="accountSessionConfig">Имплементация <see cref="IAccountSessionConfig"/>, 
        /// используемая сервисом <see cref="AccountService"/> для работы с пользовательскими сессиями.</param>
        public AccountService(
            IAccountRepository repository
            , IAccountSessionService accountSessionService
            , IAccountSessionConfig accountSessionConfig)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.accountSessionService = accountSessionService ?? throw new ArgumentNullException(nameof(accountSessionService));
            this.accountSessionConfig = accountSessionConfig ?? throw new ArgumentNullException(nameof(accountSessionConfig));
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

        public AccountSession Login(string login, string password, string userAgent, string ipAddress)
        {
            var account = GetByLogin(login);

            if (!account.Authenticate(password?.ToCharArray() ?? Array.Empty<char>()))
            {
                throw new ValidationException("Неверный пароль.");
            }

            var newAccountSession = new AccountSession(
                account
                , userAgent
                , ipAddress
                , DateTime.UtcNow.Add(accountSessionConfig.AccountSessionIdleTimeout));

            return accountSessionService.Create(newAccountSession);
        }

        public void Update(string login, char[] newPassword, IPasswordConfig actualPasswordConfig)
        {
            var account = GetByLogin(login);
            account.Password.ChangeTo(newPassword, actualPasswordConfig);
            
            repository.Save();
        }
    }
}
