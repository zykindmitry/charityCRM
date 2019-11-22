namespace DevFactoryZ.CharityCRM.Persistence
{
    public interface IRepositoryFactory
    {
        TRepository CreateRepository<TRepository>();
    }
}
