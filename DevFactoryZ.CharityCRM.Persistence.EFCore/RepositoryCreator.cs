using System;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore
{
    internal class RepositoryCreator<TRepository>  : ICreateRepository<TRepository>
    {
        private readonly Func<TRepository> creator;

        public RepositoryCreator(Func<TRepository> creator)
        {
            this.creator = creator;
        }

        public TRepository Create()
        {
            return creator();
        }
    }
}
