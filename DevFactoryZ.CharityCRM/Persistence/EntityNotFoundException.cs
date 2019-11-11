using System;

namespace DevFactoryZ.CharityCRM.Persistence
{
    public class EntityNotFoundException : EntityPersistenceException
    {
        public EntityNotFoundException(object entityKey, Type entityType) 
            : base(
                entityKey, 
                entityType,
                $"Данные не найдены")
        {
        }
    }
}
