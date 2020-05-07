using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Reflection.Metadata.Ecma335;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore
{
    internal class CharityDbContext : DbContext, IUnitOfWork
    {
        #region .ctor и конфигурация

        private string connectionString;

        /// <summary>
        /// Создает экземпляр класса CharityDbContext для создания миграций
        /// </summary>
        public CharityDbContext() : this("server = localhost\\SQLEXPRESS; database=charity-crm;Integrated security = SSPI;")
        {
        }

        public CharityDbContext(string connectionString)
            : base()
        {
            this.connectionString =
                connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder configBuilder)
        {
            configBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Загрузим все конфигурации из этой сборки
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        #endregion

        #region Реализация IUnitOfWork

        public void Save()
        {
            try
            {
                SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public TEntity GetById<TEntity, TKey>(TKey id)
            where TEntity : class, IAmPersistent<TKey>
            where TKey : struct
        {
            return GetRepository<TEntity, TKey>()?.GetById(id) ?? Find<TEntity>(id);
        }

        void IUnitOfWork.Add<TEntity>(TEntity newEntity)
        {
            Add(newEntity);
        }

        void IUnitOfWork.Remove<TEntity>(TEntity entityToRemove)
        {
            Remove(entityToRemove);
        }

        #endregion

        private IRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : class, IAmPersistent<TKey>
            where TKey : struct
        {
            var repositoryTypeInfo = typeof(CharityDbContext)
                .Assembly
                .DefinedTypes
                .FirstOrDefault(p =>
                    !p.IsGenericType
                    && !p.IsInterface
                    && p.ImplementedInterfaces.Any(e => e.Equals(typeof(IRepository<TEntity, TKey>))));

            var repository = repositoryTypeInfo?
                .AsType()
                .GetConstructor(new Type[] { typeof(DbSet<TEntity>), typeof(Action) })?
                .Invoke(new object[] { this.Set<TEntity>(), new Action(this.Save) }) as IRepository<TEntity, TKey>;

            return repository;
        }

        #region Реализация IDisposable

        internal bool IsDisposed { get; private set; }

        public override void Dispose()
        {
            IsDisposed = true;
            base.Dispose();            
        }

        #endregion
    }
}
