using Microex.Common.Abstractions;
using Microex.LogServer.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microex.LogServer.Database
{
    public class LoggingDbContext:DbContext, ISeedableDbContext
    {
        public LoggingDbContext(DbContextOptions<LoggingDbContext> options) :
            base(options)
        {

        }

        public DbSet<LoggingEntity> LoggingEntities { get; set; }

        public void Seed()
        {
            
        }
    }
}
