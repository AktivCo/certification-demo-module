using System.Reflection;
using AktivCA.Application;
using AktivCA.Domain;
using AktivCA.Domain.CAApi;
using AktivCA.Domain.EntityFrameworkCore;
using AktivCA.Domain.EntityFrameworkCore.EntityFrameworkCore;
using AktivCA.Domain.Shared.AutoReg;
using AktivCA.Domain.Shared.Configuration;
using AktivCA.Domain.Shared.Module;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;


namespace AktivCA.Web
{
    public class Program
    {
        private static readonly Type[] registeredModules = [typeof(DomainModule), typeof(ApplicationModule), typeof(EntityFrameworkCoreModule)];
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services);
            ConfigureAutoMapper(builder.Services);
            RegisterServices(builder.Services);
            RegisterDBServices(builder.Services);

            var app = builder.Build();

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

            InitModules(app.Services, builder.Services);

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            var assembly = Assembly.Load("AktivCA.Application");
            var part = new AssemblyPart(assembly);
            services.AddControllers()
                .ConfigureApplicationPartManager(apm => apm.ApplicationParts.Add(part));
        }
        private static void ConfigureAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApplicationModule).Assembly);
        }

        private static void InitModules(IServiceProvider serviceProvider, IServiceCollection serviceCollection)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var modules = services.GetServices<IApplicationModuleBase>();
                foreach (var module in modules)
                {
                    module.Init(serviceCollection);
                    module.Init();
                }
            }
        }

        private static void RegisterDBServices(IServiceCollection services)
        {
            var configuration = BuildConfiguration();

            services.AddDbContext<AktivCADbContext>(config => config.UseNpgsql(configuration.GetConnectionString("Default")));

            var certParamsSection = configuration.GetSection("CertificateParams");

            services.Configure<CertificateParams>(certParamsSection);

            services
                .AddHttpClient<ICAApiService, CAApiService>(httpClient =>
                {
                    httpClient.BaseAddress = certParamsSection.GetValue<Uri>(nameof(CertificateParams.CaUrl));
                    httpClient.DefaultRequestHeaders.Add("X-API-Key", certParamsSection.GetValue<string>(nameof(CertificateParams.ParentCaApiKey)));
                });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            RegisterTransientServicesAsImplementedInterfaces<IApplicationModuleBase>(services, registeredModules);
            RegisterTransientServices<ITransientService>(services, registeredModules);
            RegisterSingletonServices<ISingletonService>(services, registeredModules);
            RegisterScopedServices<IScopedService>(services, registeredModules);
            RegisterSelfScopedServices<ISelfScopedService>(services, registeredModules);
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

        private static void RegisterSelfScopedServices<T>(IServiceCollection services, Type[] moduleTypes)
        {
            services.Scan(scan => scan
              .FromAssembliesOf(moduleTypes)
                .AddClasses(classes => classes.AssignableTo<T>())
                    .AsSelf()
                    .WithScopedLifetime());
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }

        public static IServiceProvider TestImplementation(IServiceCollection serviceCollection)
        {
            ConfigureAutoMapper(serviceCollection);
            RegisterServices(serviceCollection);
            RegisterDBServices(serviceCollection);
            var provider = serviceCollection.BuildServiceProvider();
            return provider;
        }

    }
}
