using Airpool.Scanner.Core.Entities;
using Airpool.Scanner.Core.Generator.Base;
using Airpool.Scanner.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airpool.Scanner.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            CreateAndSeedDatabaseAsync(host).Wait();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static async Task CreateAndSeedDatabaseAsync(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var orderContext = services.GetRequiredService<ScannerContext>();
                    var generator = services.GetRequiredService<IEntityGenerator<Flight, Location>>();
                    await ScannerContextSeed.SeedAsync(orderContext, loggerFactory, generator);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex.Message);
                }
            }
        }
    }
}
