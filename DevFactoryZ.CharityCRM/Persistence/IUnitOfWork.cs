using System;

namespace DevFactoryZ.CharityCRM.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
