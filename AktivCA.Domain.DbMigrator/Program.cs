using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;


namespace AktivCA.Domain.DbMigrator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).RunConsoleAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
              .ConfigureServices((hostContext, services) =>
              {
                  services.AddHostedService<DbMigratorHostedService>();
              });
    }
}
