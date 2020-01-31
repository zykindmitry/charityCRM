namespace DevFactoryZ.CharityCRM.Persistence
{
    /// <summary>
    /// Описывает шаблон репозитория для <see cref="PasswordConfig"/>.
    /// </summary>
    public interface IPasswordConfigRepository : IRepository<PasswordConfig, int>
    {
        /// <summary>
        /// Возвращает актуальный (имеющий самую позднюю дату создания) <see cref="PasswordConfig"/> из хранилища.
        /// </summary>
        /// <returns>Экземрляр, реализующий <see cref="IPasswordConfig"/>.</returns>
        IPasswordConfig GetCurrent();
    }
}
