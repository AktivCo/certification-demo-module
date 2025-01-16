using System.Reflection;
using System.Xml.Linq;
using AktivCA.Application;
using AktivCA.Application.Certificate;
using AktivCA.Application.Contracts.Certificate;
using AktivCA.Domain;
using AktivCA.Domain.EntityFrameworkCore;
using AktivCA.Domain.EntityFrameworkCore.EntityFrameworkCore;
using AktivCA.Domain.Shared.AutoReg;
using AktivCA.Domain.Shared.Module;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;


namespace AktivCA.Web
{
    public class Program
    {
        private static readonly Type[] registeredModules = new Type[] { typeof(DomainModule), typeof(ApplicationModule), typeof(EntityFrameworkCoreModule) };
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            ConfigureServices(builder.Services);
            RegisterServices(builder.Services);
            RegisterDBServices(builder.Services);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (!app.Environment.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            //TODO: create Startup

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("swagger/v1/swagger.json", "CA API V1");
                options.RoutePrefix = string.Empty;
            });

            InitModules(app);

            app.Run();

            
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApplicationModule).Assembly);
            services.AddControllersWithViews();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            var assembly = Assembly.Load("AktivCA.Application");
            var part = new AssemblyPart(assembly);
            services.AddControllers()
                .ConfigureApplicationPartManager(apm => apm.ApplicationParts.Add(part));
        }

        private static void InitModules(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var modules = services.GetServices<IApplicationModuleBase>();
                foreach (var module in modules)
                {
                    module.Init();
                }
            }
        }

        private static void RegisterDBServices(IServiceCollection services)
        {
            var configuration = BuildConfiguration();
            services.AddDbContext<AktivCADbContext>(config => config.UseNpgsql(configuration.GetConnectionString("Default")));
        }

        private static void RegisterServices(IServiceCollection services)
        {
            RegisterTransientServicesAsImplementedInterfaces<IApplicationModuleBase>(services, registeredModules);

            RegisterTransientServices<ITransientAppService>(services, registeredModules);
            RegisterTransientServices<ITransientService>(services, registeredModules);

            RegisterSingletonServices<ISingletonAppService>(services, registeredModules);
            RegisterSingletonServices<ISingletonService>(services, registeredModules);

            RegisterScopedServices<IScopedAppService>(services, registeredModules);
            RegisterScopedServices<IScopedService>(services, registeredModules);
        }

        private static void RegisterTransientServicesAsImplementedInterfaces<T>(IServiceCollection services, Type[] moduleTypes)
        {
            services.Scan(scan => scan
             .FromAssembliesOf(moduleTypes)
                .AddClasses(classes => classes.AssignableTo<T>())
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
        }

        private static void RegisterTransientServices<T>(IServiceCollection services, Type[] moduleTypes)
        {
            services.Scan(scan => scan
             .FromAssembliesOf(moduleTypes)
                .AddClasses(classes => classes.AssignableTo<T>())
                    .AsMatchingInterface()
                    .WithTransientLifetime());
        }
        private static void RegisterSingletonServices<T>(IServiceCollection services, Type[] moduleTypes)
        {
            services.Scan(scan => scan
              .FromAssembliesOf(moduleTypes)
                .AddClasses(classes => classes.AssignableTo<T>())
                    .AsMatchingInterface()
                    .WithSingletonLifetime());
        }
        private static void RegisterScopedServices<T>(IServiceCollection services, Type[] moduleTypes)
        {
            services.Scan(scan => scan
              .FromAssembliesOf(moduleTypes)
                .AddClasses(classes => classes.AssignableTo<T>())
                    .AsMatchingInterface()
                    .WithScopedLifetime());
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
