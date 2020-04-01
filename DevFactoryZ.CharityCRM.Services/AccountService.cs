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
        private readonly IPasswordConfigRepository passwordConfigRepository;
        private readonly IAccountSessionRepository accountSessionRepository;
        private readonly IAccountSessionConfig accountSessionConfig;

        /// <summary>
        /// Создает экземпляр <see cref="AccountService"/>.
        /// </summary>
        /// <param name="repository">Имплементация <see cref="IAccountRepository"/>, используемая сервисом <see cref="AccountService"/> для работы с хранилищем.</param>
        /// <param name="accountSessionRepository">Имплементация <see cref="IAccountSessionService"/>, 
        /// используемая сервисом <see cref="AccountService"/> для работы с хранилищем пользовательских сессий.</param>
        /// <param name="accountSessionConfig">Имплементация <see cref="IAccountSessionConfig"/>, 
        /// используемая сервисом <see cref="AccountService"/> для работы с пользовательскими сессиями.</param>
        public AccountService(IAccountRepository repository, 
            IPasswordConfigRepository passwordConfigRepository,
            IAccountSessionRepository accountSessionRepository, 
            IAccountSessionConfig accountSessionConfig)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.passwordConfigRepository = 
                passwordConfigRepository 
                    ?? throw new ArgumentNullException(nameof(passwordConfigRepository));
            this.accountSessionRepository = 
                accountSessionRepository ?? throw new ArgumentNullException(nameof(accountSessionRepository));
            this.accountSessionConfig = 
                accountSessionConfig ?? throw new ArgumentNullException(nameof(accountSessionConfig));
        }

        public Account Create(AccountData data)
        {
            var newAccount = new Account(
                data.Login, 
                data.PasswordClearText, 
                passwordConfigRepository.GetCurrent());

            repository.Add(newAccount);
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
                , Guid.NewGuid()
                , userAgent
                , ipAddress
                , DateTime.UtcNow.Add(accountSessionConfig.AccountSessionIdleTimeout));

            accountSessionRepository.Add(newAccountSession);
            accountSessionRepository.Save();

            return newAccountSession;
        }

        public void Update(string login, char[] newPassword)
        {
            var account = GetByLogin(login);
            account.Password.ChangeTo(newPassword, passwordConfigRepository.GetCurrent());
            
            repository.Save();
        }
    }
}
