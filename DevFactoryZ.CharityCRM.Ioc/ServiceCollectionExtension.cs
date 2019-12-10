using DevFactoryZ.CharityCRM.Persistence;
using DevFactoryZ.CharityCRM.Persistence.EFCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevFactoryZ.CharityCRM.Ioc
{
    /// <summary>
    /// Этот класс реализует метод расширения IServiceCollection
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Регистрирует реализацию UnitOfWork и всех репозиториев в IServiceCollection
        /// </summary>
        /// <param name="services">Ссылка на IServiceCollection</param>
        /// <param name="connectionName">Имя строки соединения с БД в конфигурационном файле</param>
        public static void RegisterDataAccessComponents(this IServiceCollection services, string connectionName)
        {
            // Register UnitOfWorkCreator single per lifetime
            services.AddScoped(
                provider => 
                    new UnitOfWorkCreator(provider.GetService<IConfiguration>(), connectionName));
            services.AddScoped<ICreateUnitOfWork>(
                provider => provider.GetService<UnitOfWorkCreator>());
            services.AddScoped<IRepositoryFactory>(
                provider => provider.GetService<UnitOfWorkCreator>());

            // Register repositories new instance per resolve
            services.RegisterRepository<IPermissionRepository>();
        }

        private static void RegisterRepository<TRepository>(this IServiceCollection services)
            where TRepository : class
        {
            services.AddTransient(
               provider => 
                provider
                    .GetService<IRepositoryFactory>()
                    .CreateRepository<TRepository>());
        }
    }
}
