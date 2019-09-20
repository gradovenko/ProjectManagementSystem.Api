using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;

namespace ProjectManagementSystem.DatabaseMigrations
{
    public sealed class MigrationsBackgroundService : BackgroundService
    {
        private readonly ProjectManagementSystemDbContext _context;
        private readonly ILogger<MigrationsBackgroundService> _logger;

        public MigrationsBackgroundService(ProjectManagementSystemDbContext context, ILogger<MigrationsBackgroundService> logger)
        {
            _logger = logger;
            _context = context;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting migrations");

            try
            {
                await Policy
                    .Handle<SocketException>()
                    .WaitAndRetryAsync(10,
                        tryNumber => TimeSpan.FromSeconds(tryNumber),
                        (ex, delayTime) => _logger.LogWarning(ex, $"The connection to the database failed. Try again in {delayTime.Seconds} seconds."))
                    .ExecuteAsync(async ct => await _context.Database.MigrateAsync(ct), cancellationToken);

                _logger.LogInformation("Migrations ended");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Migrations failed");
            }
        }
    }
}