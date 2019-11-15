using Microsoft.EntityFrameworkCore;
using System;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore
{
    internal class CharityDbContext : DbContext
    {
        private string connectionString;

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
    }
}
