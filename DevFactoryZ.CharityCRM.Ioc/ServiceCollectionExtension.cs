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
                .WithRepository<IDonationRepository>()
                .WithRepository<IFundRegistrationRepository>()
                .WithRepository<IAccountRepository>()
                .WithRepository<IAccountSessionRepository>()
                .WithRepository<IPasswordConfigRepository>();
        }

        /// <summary>
        /// Регистрирует доменные сервисы
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        /// <returns>Коллекция сервисов</returns>
        public static IServiceCollection WithDomainServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IPermissionService>(
                    provider => new PermissionService(provider.GetService<IPermissionRepository>()))
                .AddTransient<IRoleService>(
                    provider => new RoleService(provider.GetService<IRoleRepository>()))
                .AddTransient<IAccountSessionService>(
                    provider => new AccountSessionService(provider.GetService<IAccountSessionRepository>()))
                .AddTransient<IAccountService>(
                    provider => new AccountService(
                        provider.GetService<IAccountRepository>(), 
                        provider.GetService<IPasswordConfigRepository>(),
                        provider.GetService<IAccountSessionRepository>(), 
                        WithConfig<AccountSessionConfig>(provider.GetService<IConfiguration>())));
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

        private static T WithConfig<T>(IConfiguration configuration)
        {
            var sect = configuration.GetSection(typeof(T).Name);
            var res = sect.Get<T>();
            return res;
        }
    }
}
