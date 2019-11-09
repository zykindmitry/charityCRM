using Microsoft.EntityFrameworkCore;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore
{
    internal class CharityDbContext : DbContext
    {        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Загрузим все конфигурации из этой сборки
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly); 
        }
    }
}
