using System;
using DevFactoryZ.CharityCRM.Persistence;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Содержит методы расширений для работы с <see cref="AccountSession"/>.
    /// </summary>
    public static class AccountSessionExtensions
    {
        /// <summary>
        /// Сохранение изменений текущей пользовательской сессии в хранилище данных.
        /// </summary>
        /// <param name="accountSession">Текущая <see cref="AccountSession"/>.</param>
        /// <param name="repositoryCreatorFactory">Фабрика создателей репозиториев <see cref="IRepositoryCreatorFactory"/>.</param>
        /// <returns>Текущая <see cref="AccountSession"/>.</returns>
        public static AccountSession UpdateRepository(
            this AccountSession accountSession
            , IRepositoryCreatorFactory repositoryCreatorFactory)
        {
            var accountSessionRepository = repositoryCreatorFactory?
                .GetRepositoryCreator<IAccountSessionRepository>()
                .Create() 
                ?? throw new ArgumentNullException(nameof(repositoryCreatorFactory));

            accountSession = accountSessionRepository.Update(accountSession);
            accountSessionRepository.Save();

            return accountSession;
        }
    }    
}
