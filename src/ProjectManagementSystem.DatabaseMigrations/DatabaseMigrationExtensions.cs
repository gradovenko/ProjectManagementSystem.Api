using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

namespace ProjectManagementSystem.DatabaseMigrations
{
    internal static class DatabaseMigrationExtensions
    {
        public static void TryMigrate(this DatabaseFacade databaseFacade, ILogger logger, int countTry)
        {
            try
            {
                databaseFacade.Migrate();
            }
            catch (SocketException exception)
            {
                logger.LogError(exception, $"Attempt №{countTry--} connect to the database server");

                if (countTry == 0)
                    throw;

                Task.Run(async () =>
                {
                    await Task.Delay(countTry * 1000);
                    databaseFacade.TryMigrate(logger, countTry);
                }).Wait();
            }
        }
    }
}