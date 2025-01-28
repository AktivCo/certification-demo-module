using AktivCA.Domain.EntityFrameworkCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AktivCA.Domain.DbMigrator
{
    public class DbMigratorHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly IConfiguration _configuration;
        private readonly IServiceCollection _services;

        public DbMigratorHostedService(IHostApplicationLifetime hostApplicationLifetime, IConfiguration configuration)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _configuration = configuration;
            _services = new ServiceCollection();
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var context = RegisterAndGetDBServices(_services);
            context.Database.Migrate();
            _hostApplicationLifetime.StopApplication();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private AktivCADbContext RegisterAndGetDBServices(IServiceCollection services)
        {
            var configuration = BuildConfiguration();
            services.AddSingleton<IConfigurationRoot>(configuration);
            services.AddSingleton<IConfiguration>(configuration);
            services.AddDbContext<AktivCADbContext>(config => config.UseNpgsql(configuration.GetConnectionString("Default")));
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetService<AktivCADbContext>();
        }
        private IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
