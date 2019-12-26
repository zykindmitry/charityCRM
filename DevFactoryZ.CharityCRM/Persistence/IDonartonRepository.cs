using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.Persistence
{
    /// <summary>
    /// Интерфейс описывает обобщенный репозиторий для всех типов пожертвований.
    /// </summary>
    public interface IDonationRepository : IRepository<Donation, long>
    {
        /// <summary>
        /// Возвращает сущности указанного типа.
        /// </summary>
        /// <typeparam name="Entity">Тип сущности.</typeparam>
        /// <returns>Набор сущностей указанного типа.</returns>
        IEnumerable<Entity> GetAll<Entity>();
    }
}
