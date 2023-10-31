using Microsoft.EntityFrameworkCore;
using Net7PostgressCompose.Models;
using System.Collections.Generic;

namespace Net7PostgressCompose.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
        public DbSet<Driver> Drivers { get; set; }

    }
}
