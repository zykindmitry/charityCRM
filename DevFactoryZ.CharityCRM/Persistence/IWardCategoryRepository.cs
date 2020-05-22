using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.Persistence
{
    /// <summary>
    /// Интерфейс описывает шаблон репозиторий для категорий подопечных БФ.
    /// </summary>
    public interface IWardCategoryRepository : IRepository<WardCategory, int>
    {
        /// <summary>
        /// Возвращает только рутовые категории, т.е. те, которые 
        /// не являются покатегориями других категорий.
        /// </summary>
        /// <returns>Коллекция категорий <see cref="WardCategory"/>.</returns>
        IEnumerable<WardCategory> GetRoots();

        /// <summary>
        /// Проверка наличия родительской <see cref="WardCategory"/> у проверяемой 
        /// <see cref="WardCategory"/>.
        /// </summary>
        /// <param name="wardCategoryId">Идентификатор проверяемой <see cref="WardCategory"/>.</param>
        /// <returns>Результат проверки</returns>
        bool HasParent(int wardCategoryId);

        /// <summary>
        /// Рекурсивная проверка наличия циклической зависимости между категориями <see cref="WardCategory"/>.
        /// </summary>
        /// <param name="parentId">Идентификатор родительской <see cref="WardCategory"/>.</param>
        /// <param name="childId">Идентификатор дочерней <see cref="WardCategory"/>.</param>
        /// <param name="isCycling">Флаг прекращения рекурсивной проверки.</param>
        /// <returns>Результат проверки.</returns>
        bool HasCycling(int parentId, int childId, bool isCycling = false);
    }
}
