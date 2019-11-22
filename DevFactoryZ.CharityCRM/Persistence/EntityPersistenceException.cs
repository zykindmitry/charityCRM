using System;

namespace DevFactoryZ.CharityCRM.Persistence
{
    public class EntityPersistenceException : Exception
    {
        public EntityPersistenceException(
            object entityKey, 
            Type entityType,
            string errorMessage, 
            Exception innerException = null)
            : base(errorMessage, innerException)
        {
            EntityKey = entityKey;
            EntityType = entityType;
        }

        public object EntityKey { get; }

        public Type EntityType { get; }
    }
}
