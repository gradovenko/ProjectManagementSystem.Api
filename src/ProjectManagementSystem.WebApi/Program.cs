using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace ProjectManagementSystem.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog((webHostBuilderContext, loggerConfiguration) =>
                    loggerConfiguration.ReadFrom.Configuration(webHostBuilderContext.Configuration)
                        .Enrich.FromLogContext()
                );
    }
}