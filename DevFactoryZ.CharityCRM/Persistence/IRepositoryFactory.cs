namespace DevFactoryZ.CharityCRM.Persistence
{
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Создает репозиторий для заданной сущности
        /// </summary>
        /// <returns>Ссылка на объект-репозиторий</returns>
        TRepository CreateRepository<TRepository>();
    }
}
