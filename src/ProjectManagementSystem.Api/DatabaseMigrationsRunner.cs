using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using Polly;
using ProjectManagementSystem.DatabaseMigrations;

namespace ProjectManagementSystem.Api;

internal static class DatabaseMigrationsRunner
{
    public static IHost RunDatabaseMigrations(this IHost host)
    {
        using var serviceScope = host.Services.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<MigrationDbContext>();
        var loggerFactory = serviceScope.ServiceProvider.GetService<ILoggerFactory>();

        var logger = loggerFactory.CreateLogger("DatabaseMigrations");

        logger.LogInformation("Starting migrations");

        try
        {
            Policy
                .Handle<SocketException>()
                .WaitAndRetry(10,
                    (tryNumber) => TimeSpan.FromSeconds(tryNumber),
                    (ex, delayTime) => logger.LogWarning(ex, $"Connection to database server failed. Try after {delayTime.Seconds} seconds."))
                .Execute(() => context.Database.Migrate());

            logger.LogInformation("Migrations ended");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Migrations failed");
            throw;
        }

        return host;
    }
}