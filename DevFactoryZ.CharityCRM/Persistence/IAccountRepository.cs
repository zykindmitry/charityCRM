﻿using System.Collections.Generic;
using DevFactoryZ.CharityCRM;

namespace DevFactoryZ.CharityCRM.Persistence
{
    public interface IAccountRepository<TKey> : IRepository<Account, TKey>
    {
        /// <summary>
        /// Возвращает объект типа <see cref="Account"/> из хранилища по заданному <see cref="Account.Login"/>.
        /// Генерирует <see cref="EntityNotFoundException"/>, если объект не найден.
        /// </summary>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <param name="login">Значение <see cref="Account.Login"/> для поиска нужной записи.</param>
        /// <returns>Найденный в хранилище объект типа <see cref="Account"/>.</returns>
        Account GetByLogin(TKey login);        
    }
}
