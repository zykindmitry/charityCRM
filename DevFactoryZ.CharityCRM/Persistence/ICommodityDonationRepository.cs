namespace DevFactoryZ.CharityCRM.Persistence
{

    /// <summary>
    /// Интерфейс описывает шаблон репозиторий для пожертвований в виде предметов 
    /// (одежды, продуктов питания, книг и т.п.).
    /// </summary>
    public interface ICommodityDonationRepository : IRepository<CommodityDonation, long>
    {
    }
}
