using DevFactoryZ.CharityCRM.Ioc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DevFactoryZ.CharityCRM.UI.Web.Configuration;
using DevFactoryZ.CharityCRM.UI.Web.Middleware;
using DevFactoryZ.CharityCRM.Services;
using DevFactoryZ.CharityCRM.Persistence;

namespace DevFactoryZ.CharityCRM.UI.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            sessionConfig = configuration.GetSection(nameof(SessionConfig)).Get<SessionConfig>();
            cookieConfig = configuration.GetSection(nameof(CookieConfig)).Get<CookieConfig>();
        }

        public IConfiguration Configuration { get; }
        
        ISessionConfig sessionConfig;
        ICookieConfig cookieConfig;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton(Configuration);
            
            services
                .WithDataAccessComponents("local")
                .WithDomainServices();
            
            services
                .AddDistributedMemoryCache()
                .AddSession(options =>
                {
                    options.IdleTimeout = sessionConfig.ServerSessionIdleTimeout;
                });
                
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app
            , IWebHostEnvironment env
            , IRepositoryCreatorFactory repositoryCreatorFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            
            app.UseSession();
            
            app.UseCharityAuthentication(repositoryCreatorFactory, sessionConfig, cookieConfig);
 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
