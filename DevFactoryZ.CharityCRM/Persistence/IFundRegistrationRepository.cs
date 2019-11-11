using System;
using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.Persistence
{
    /// <summary>
    /// Этот интерфейс описывает методы шаблона Repository для сущности FundRegistration
    /// </summary>
    public interface IFundRegistrationRepository
    {
        /// <summary>
        /// Добавляет заявку на регистрацию БФ в UnitOfWork для последующего сохранения в хранилище
        /// см. IUnitOfWork.Save
        /// Если IsValid заявки равен false, то генерирует исключение InvalidEntityException
        /// </summary>
        /// <param name="entity">Объект, который необходимо добавить.</param>
        void Create(FundRegistration entity);

        /// <summary>
        /// Ищет заявку на регистрацию БФ в хранилище по идентификатору. 
        /// Возвращает заявку если она найдена иначе генерирует исключение EntityNotFoundException
        /// </summary>
        /// <param name="id">Значение Id заявки на регистрацию БФ (см. FundRegistration.Id)</param>
        /// <returns>Заявка на регистрацию БФ, удовлетворяющая критерию поиска</returns>
        FundRegistration GetByGuid(Guid id);

        /// <summary>
        /// Возвращает все заявки на регистрацию БФ
        /// </summary>
        /// <returns>Перечисление заявок на регистрацию БФ</returns>
        IEnumerable<FundRegistration> GetAll();

        /// <summary>
        /// Помечает на удаление заявку на регистрацию БФ по значению идентификатора       
        /// см. IUnitOfWork.Save
        /// Если IsValid заявки равен false, то генерирует исключение InvalidEntityException
        /// </summary>
        /// <param name="id">Идентификатор удаляемой заявки на регистрацию БФ</param>
        void Delete(Guid id);
    }
}
