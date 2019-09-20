using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ProjectManagementSystem.DatabaseMigrations
{
    internal sealed class Program
    {
        private static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureAppConfiguration(builder =>
                {
                    builder
                        .AddJsonFile("appsettings.json", true)
                        .AddEnvironmentVariables()
                        .AddCommandLine(args);
                })
                .ConfigureServices((context, collection) => ConfigureServices(collection, context.Configuration))
                .UseSerilog((whb, ctx) =>
                    ctx.ReadFrom.Configuration(whb.Configuration)
                        .Enrich.FromLogContext())
                .UseConsoleLifetime()
                .Build();

            await host.RunAsync();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddLogging(builder => builder
                .AddSerilog(new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger()));

            serviceCollection.AddDbContext<ProjectManagementSystemDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("ProjectMS"));
            });

            serviceCollection.AddHostedService<MigrationsBackgroundService>();
        }
    }
}