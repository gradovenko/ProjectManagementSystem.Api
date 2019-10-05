using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ProjectManagementSystem.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().RunDatabaseMigrations().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webHostBuilder =>
                {
                    webHostBuilder
                        .UseStartup<Startup>()
                        .UseSerilog((webHostBuilderContext, loggerConfiguration) =>
                            loggerConfiguration.ReadFrom.Configuration(webHostBuilderContext.Configuration)
                                .Enrich.FromLogContext());
                });
    }
}