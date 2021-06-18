using Airpool.Scanner.Security.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airpool.Scanner.Security.Data
{
    public class SecurityContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public SecurityContext(DbContextOptions<SecurityContext> options) : base(options)
        {
        }
    }
}
