using Microsoft.EntityFrameworkCore;
using System;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore
{
    internal class CharityDbContext : DbContext, IUnitOfWork
    {
        #region .ctor и конфигурация

        private string connectionString;

        /// <summary>
        /// Создает экземпляр класса CharityDbContext для создания миграций
        /// </summary>
        public CharityDbContext() : this("<... Скопируйте строку соединения сюда ... >")
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
            return Find<TEntity>(id);
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
