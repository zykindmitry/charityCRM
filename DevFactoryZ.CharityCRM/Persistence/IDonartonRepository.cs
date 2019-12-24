namespace DevFactoryZ.CharityCRM.Persistence
{
    /// <summary>
    /// Интерфейс описывает обобщенный репозиторий для всех типов пожертвований.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IDonationRepository : ICashDonationRepository, ICommodityDonationRepository
    {
    }
}
