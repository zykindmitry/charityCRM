using DevFactoryZ.CharityCRM.Ioc;
using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using React.AspNet;
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

            sessionConfig = configuration.GetSection(nameof(ServerSessionConfig)).Get<ServerSessionConfig>();
            cookieConfig = configuration.GetSection(nameof(CookieConfig)).Get<CookieConfig>();
        }

        public IConfiguration Configuration { get; }
        
        IServerSessionConfig sessionConfig;
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
                

            services.AddMemoryCache();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddReact();
            services.AddJsEngineSwitcher(options => options.DefaultEngineName = ChakraCoreJsEngine.EngineName).AddChakraCore();
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

            app.UseReact(config => { });
            app.UseStaticFiles();
            app.UseRouting();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
