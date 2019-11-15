using System;
using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore
{
    public class UnitOfWorkCreator : ICreateUnitOfWork, IRepositoryFactory
    {
        #region .ctor

        private readonly string connectionString;        

        public UnitOfWorkCreator(string connectionString)
        {
            this.connectionString = connectionString;
        }

        #endregion

        #region DbContext

        private CharityDbContext dbContext;

        private void InitDbContextIfNeeded()
        {
            if (dbContext == null || dbContext.IsDisposed)
            {
                CreateDbContext();
            }
        }

        private void CreateDbContext()
        {
            dbContext = new CharityDbContext(connectionString);
        }

        #endregion

        #region Implementation of ICreateUnitOfWork

        public IUnitOfWork Create()
        {
            CreateDbContext();
            return dbContext;
        }

        #endregion

        #region Implementation of IRepositoryFactory

        private readonly Dictionary<Type, Func<CharityDbContext, object>> factories =
            new Dictionary<Type, Func<CharityDbContext, object>>();

        public TRepository CreateRepository<TRepository>()
        {
            if (!factories.ContainsKey(typeof(TRepository)))
            {
                throw new NotSupportedException(
                    $"Репозиторий {typeof(TRepository)} не поддерживается сборкой {GetType().Assembly.FullName}");
            }

            InitDbContextIfNeeded();
            return (TRepository)factories[typeof(TRepository)](dbContext);
        }

        #endregion
    }
}
