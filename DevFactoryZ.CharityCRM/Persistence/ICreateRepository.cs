namespace DevFactoryZ.CharityCRM.Persistence
{
    /// <summary>
    /// Описывает обобщенный фабричный метод репозитория
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    /// <typeparam name="TKey">Тип ключа сущности</typeparam>
    public interface ICreateRepository<TRepository>
    {
        TRepository Create();
    }
}
