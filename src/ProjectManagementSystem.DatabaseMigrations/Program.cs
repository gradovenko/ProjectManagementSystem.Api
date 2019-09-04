using System;
using System.IO;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ProjectManagementSystem.DatabaseMigrations
{
    internal sealed class Program
    {
        private static readonly ManualResetEventSlim _manualResetEventSlim = new ManualResetEventSlim(false);
        
        private static void Main(string[] args)
        {
            var configuration = CreateConfiguration();
            var serviceProvider = ConfigureServices(configuration);
            var sp = serviceProvider.BuildServiceProvider();
            var loggerFactory = sp.GetService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<ProjectManagementSystemDbContext>();

            using (var cancellationToken = new CancellationTokenSource())
            {
                void Shutdown()
                {
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        logger.LogInformation("Application is shutting down");
                        cancellationToken.Cancel();
                    }

                    _manualResetEventSlim.Wait(cancellationToken.Token);
                }

                Console.CancelKeyPress += (sender, eventArgs) =>
                {
                    Shutdown();
                    eventArgs.Cancel = true;
                };

                logger.LogInformation("Starting migrations");

                sp.GetService<ProjectManagementSystemDbContext>().Database.TryMigrate(logger, 10);

                logger.LogInformation("Migrations ended");

                cancellationToken.Token.WaitHandle.WaitOne();

                _manualResetEventSlim.Set();
            }
        }
        
        private static IConfigurationRoot CreateConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
        }
        
        private static IServiceCollection ConfigureServices(IConfiguration configuration)
        {
            var services = new ServiceCollection();
            
            services.AddLogging(builder => builder
                .AddSerilog(new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger()));

            services.AddDbContext<ProjectManagementSystemDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("ProjectMS"));
            });

            return services;
        }
        
        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ProjectManagementSystemDbContext>
        {
            public ProjectManagementSystemDbContext CreateDbContext(string[] args)
            {
                var configuration = CreateConfiguration();
                var serviceProvider = ConfigureServices(configuration);

                return serviceProvider.BuildServiceProvider().GetService<ProjectManagementSystemDbContext>();
            }
        }
    }
}