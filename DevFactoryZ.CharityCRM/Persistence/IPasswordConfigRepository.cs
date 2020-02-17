using System.Collections.Generic;
using System;

namespace DevFactoryZ.CharityCRM.Persistence
{
    /// <summary>
    /// Описывает шаблон репозитория для <see cref="PasswordConfig"/>.
    /// </summary>
    public interface IPasswordConfigRepository
    {
        /// <summary>
        /// Добавляет объект <see cref="PasswordConfig"/> в unit of work для последующей вставки в хранилище данных системы.
        /// см. Save
        /// </summary>
        /// <param name="repositoryType">Экземпляр <see cref="PasswordConfig"/> для сохранения в хранилище.</param>
        void Create(PasswordConfig repositoryType);

        /// <summary>
        /// Возвращает экземпляр <see cref="PasswordConfig"/>, если он найден по его идентификатору <see cref="PasswordConfig.Id"/>.
        /// </summary>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <param name="id">Идентификатор <see cref="PasswordConfig"/> в хранилище.</param>
        /// <returns>Экземпляр <see cref="PasswordConfig"/>, найденный в хранилище по идентификатору.</returns>
        PasswordConfig GetById(int id);

        /// <summary>
        /// Возвращает все сущности <see cref="PasswordConfig"/> из хранилища системы.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>Коллекция всех сущностей <see cref="PasswordConfig"/> из хранилища.</returns>
        IEnumerable<PasswordConfig> GetAll();

        /// <summary>
        /// Возвращает актуальный (имеющий самую позднюю дату создания) <see cref="PasswordConfig"/> из хранилища.
        /// </summary>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <returns>Экземрляр, реализующий <see cref="PasswordConfig"/>.</returns>
        /// <returns>Экземпляр <see cref="PasswordConfig"/> с самой поздней датой создания.</returns>
        PasswordConfig GetCurrent();

        /// <summary>
        /// Сохраняет все new и dirty объекты в хранилище данных и перерегистрирует их как clean
        /// </summary>
        void Save();
    }
}
