using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airpool.Scanner.Infrastructure.Data
{
    public class ScannerContextSeed
    {
        public static async Task SeedAsync(ScannerContext scannerContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                scannerContext.Database.Migrate();

                // TODO: add tables seeding
                
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 3)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<ScannerContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(scannerContext, loggerFactory, retryForAvailability);
                }
            }
        }

        // TODO: make seeding methods
        // private static IEnumerable<--Something--> GetPreconfigured--Something--()
    }
}
