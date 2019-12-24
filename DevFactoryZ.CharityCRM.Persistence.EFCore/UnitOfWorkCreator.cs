using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore
{
    public class UnitOfWorkCreator : ICreateUnitOfWork, IRepositoryFactory, IDisposable
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

        #region Implementation of IRepositoryFactory

        private readonly Dictionary<Type, Func<CharityDbContext, object>> factories =
            new Dictionary<Type, Func<CharityDbContext, object>>
            {
                { typeof(IPermissionRepository), db => new PermissionRepository(db.Set<Permission>(), db.Save) },
                { typeof(IRoleRepository), db => new RoleRepository(db.Set<Role>(), db.Save) },
                { typeof(ICashDonationRepository), db => new DonationRepository<CashDonation>(db.Set<CashDonation>(), db.Save) },
                { typeof(ICommodityDonationRepository), db => new DonationRepository<CommodityDonation>(db.Set<CommodityDonation>(), db.Save) }
            };

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

        #region Реализация IDisposable

        public void Dispose()
        {
            dbContext?.Dispose();
        }

        #endregion
    }
}
