using DevFactoryZ.CharityCRM.Persistence;
using DevFactoryZ.CharityCRM.Persistence.EFCore;
using DevFactoryZ.CharityCRM.Services;
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
        public static IServiceCollection WithDataAccessComponents(this IServiceCollection services, string connectionName)
        {
            // Register UnitOfWorkCreator single per lifetime
            return services
                .AddScoped(
                    provider => 
                        new UnitOfWorkCreator(provider.GetService<IConfiguration>(), connectionName))
                .AddScoped<ICreateUnitOfWork>(
                    provider => provider.GetService<UnitOfWorkCreator>())
                .AddScoped<IRepositoryFactory>(
                    provider => provider.GetService<UnitOfWorkCreator>())
                .AddScoped<IRepositoryCreatorFactory>(
                    provider => provider.GetService<UnitOfWorkCreator>())                    
                .WithRepository<IPermissionRepository>()
                .WithRepository<IRoleRepository>()
                .WithRepository<IDonationRepository>();
        }

        public static IServiceCollection WithJsonConfig(this IServiceCollection services, params string[] configFilenames)
        {
            var configBuilder = new ConfigurationBuilder();

            foreach (var filename in configFilenames)
            {
                configBuilder.AddJsonFile(filename, true, true);
            }

            return services.AddSingleton<IConfiguration>(configBuilder.Build());
        }

        private static IServiceCollection WithRepository<TRepository>(this IServiceCollection services)
            where TRepository : class
        {
            return services
                .AddScoped(
                    provider => 
                        provider
                            .GetService<IRepositoryFactory>()
                            .CreateRepository<TRepository>())
                .AddScoped(
                    provider => 
                        provider
                            .GetService<IRepositoryCreatorFactory>()
                            .GetRepositoryCreator<TRepository>());
        }
    }
}
