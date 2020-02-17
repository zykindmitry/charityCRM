using System;

namespace DevFactoryZ.CharityCRM.Persistence
{
    /// <summary>
    /// Описывает шаблон репозитория для <see cref="AccountSession"/>.
    /// </summary>
    public interface IAccountSessionRepository : IRepository<AccountSession, Guid>
    {
        /// <summary>
        /// Изменяет существующий <see cref="AccountSession"/> в хранилище.
        /// </summary>
        /// <param name="repositoryType">Данные для изменения.</param>
        /// <returns>Измененный и сохраненный в хранилище экземпляр <see cref="AccountSession"/>.</returns>
        AccountSession Update(AccountSession repositoryType);
    }
}
