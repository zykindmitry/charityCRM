namespace DevFactoryZ.CharityCRM.Persistence
{
    /// <summary>
    /// Описывает фабрику фабричных методов репозиториев
    /// </summary>
    public interface IRepositoryCreatorFactory
    {
        /// <summary>
        /// Создает фабричный метод репозитория по заданному типу сущности
        /// </summary>
        /// <typeparam name="TRepository">Тип репозитория</typeparam>
        /// <returns>Объект, реализующий фабричный метод репозитория заданной сущности</returns>
        ICreateRepository<TRepository> GetRepositoryCreator<TRepository>();
    }
}
