using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airpool.Scanner.Infrastructure.Data
{
    public class ScannerContext : DbContext
    {
        //public DbSet<Ticket> Tickets { get; set; }

        public ScannerContext(DbContextOptions<ScannerContext> options) : base(options)
        {
        }
    }
}
