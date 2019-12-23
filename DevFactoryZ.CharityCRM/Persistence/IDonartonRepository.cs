using System;
using System.Collections.Generic;
using System.Text;

namespace DevFactoryZ.CharityCRM.Persistence
{
    /// <summary>
    /// Интерфейс описывает обобщенный репозиторий для всех типов пожертвований.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IDonationRepository<TEntity, TKey> : IRepository<TEntity, TKey> 
        where TEntity : class, IAmPersistent<TKey>
        where TKey : struct
    {
    }
}
