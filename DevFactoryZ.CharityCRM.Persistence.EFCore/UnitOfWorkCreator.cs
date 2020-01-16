using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore
{
    public class UnitOfWorkCreator : 
        ICreateUnitOfWork, 
        IRepositoryFactory, 
        IRepositoryCreatorFactory, 
        IDisposable
    {
        #region .ctor

        private readonly string connectionString;        

        public UnitOfWorkCreator(IConfiguration config, string connectionName)
        {
            connectionString = config.GetConnectionString(connectionName);
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

        #region Реализация IRepositoryFactory и IRepositoryCreatorFactory

        private readonly Dictionary<Type, Func<CharityDbContext, object>> factories =
            new Dictionary<Type, Func<CharityDbContext, object>>
            {
                { typeof(IPermissionRepository), db => new PermissionRepository(db.Set<Permission>(), db.Save) },
                { typeof(IRoleRepository), db => new RoleRepository(db.Set<Role>(), db.Save) },
                { typeof(IDonationRepository), db => new DonationRepository<Donation>(db.Set<Donation>(), db.Save) },
                { typeof(IFundRegistrationRepository), db => new FundRegistrationRepository(db.Set<FundRegistration>(), db.Save) }
            };

        public TRepository CreateRepository<TRepository>()
        {
            var creator = GetCreatorIfExists<TRepository>();
            InitDbContextIfNeeded();

            return (TRepository)creator(dbContext);
        }

        public ICreateRepository<TRepository> GetRepositoryCreator<TRepository>()
        {
            var creator = GetCreatorIfExists<TRepository>();

            return new RepositoryCreator<TRepository>(
                () => 
                {
                    InitDbContextIfNeeded();
                    return (TRepository)creator(dbContext);
                });
        }

        private Func<CharityDbContext, object> GetCreatorIfExists<T>()
        {
            return factories.GetValueOrDefault(typeof(T))
                ?? throw new NotSupportedException(
                    $"Репозиторий {typeof(T)} не поддерживается сборкой {GetType().Assembly.FullName}");
        }

        #endregion

        #region Реализация IDisposable

        public void Dispose()
        {
            dbContext?.Dispose();
        }

        #endregion
    }
}
