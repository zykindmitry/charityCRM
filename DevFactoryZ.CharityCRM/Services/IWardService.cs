﻿using System;
using System.Collections.Generic;
using DevFactoryZ.CharityCRM.Persistence;

namespace DevFactoryZ.CharityCRM.Services
{
    /// <summary>
    /// Вспомогательный тип для передачи значений свойств типу <see cref="Ward"/> 
    /// при создании/изменении подопечного БФ.
    /// </summary>
    public class WardData
    {
        /// <summary>
        /// ФИО подопечного.
        /// </summary>
        public FullName FullName { get; set; }

        /// <summary>
        /// Адрес подопечного.
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// Дата регистрации подопечного в системе.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата рождения подопечного.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Номер телефона подопечного
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Набор категорий <see cref="WardCategory"/>'s для подопечного.
        /// </summary>
        public IEnumerable<WardCategory> WardCategories { get; set; }
    }

    /// <summary>
    /// Описывает CRUD-методы и методы добавления/удаления категорий <see cref="WardCategory"/>,
    /// для подопечного БФ <see cref="Ward"/>.
    /// </summary>
    public interface IWardService
    {
        /// <summary>
        /// Получение всех <see cref="Ward"/>'s из хранилища.
        /// </summary>
        /// <returns>Коллекция <see cref="Ward"/>'s.</returns>
        IEnumerable<Ward> GetAll();

        /// <summary>
        /// Получение из хранилища объекта <see cref="Ward"/> по заданному идентификатору 
        /// <see cref="Ward.Id"/>.
        /// Генерирует <see cref="EntityNotFoundException"/>, если объект не найден.
        /// </summary>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <param name="id">Идентификатор подопесного в хранилище.</param>
        /// <returns>Найденный в хранилище объект <see cref="Ward"/>.</returns>
        Ward GetById(int id);

        /// <summary>
        /// Сохранение нового объекта <see cref="Ward"/> в хранилище.
        /// </summary>
        /// <param name="data">Данные подопечного для сохранения.</param>
        /// <returns>Сохраненная <see cref="WardCategory"/>.</returns>
        Ward Create(WardData data);

        /// <summary>
        /// Изменение существующего объекта <see cref="Ward"/> в хранилище.
        /// Генерирует <see cref="EntityNotFoundException"/>, если объект не найден.
        /// </summary>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <param name="id">Идентификатор подопечного в хранилище.</param>
        /// <param name="data">Данные подопечного для изменения.</param>
        void Update(int id, WardData data);

        /// <summary>
        /// Удаление объекта <see cref="Ward"/> из хранилища.
        /// </summary>
        /// <param name="id">Идентификатор подопечного для удаления.</param>
        void Delete(int id);
    }
}
