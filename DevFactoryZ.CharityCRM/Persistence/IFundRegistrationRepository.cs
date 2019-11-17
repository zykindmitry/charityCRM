using System;

namespace DevFactoryZ.CharityCRM.Persistence
{
    /// <summary>
    /// Этот интерфейс описывает методы шаблона Repository для сущности FundRegistration
    /// </summary>
    public interface IFundRegistrationRepository : IRepository<FundRegistration>
    {
        /// <summary>
        /// Ищет заявку на регистрацию БФ в хранилище по идентификатору. 
        /// Возвращает заявку если она найдена иначе генерирует исключение EntityNotFoundException
        /// </summary>
        /// <param name="id">Значение Id заявки на регистрацию БФ (см. FundRegistration.Id)</param>
        /// <returns>Заявка на регистрацию БФ, удовлетворяющая критерию поиска</returns>
        FundRegistration GetByGuid(Guid id);
    }
}

