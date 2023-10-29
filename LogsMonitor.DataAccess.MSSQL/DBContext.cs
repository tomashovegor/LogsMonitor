using LogsMonitor.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogsMonitor.DataAccess.MSSQL
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) 
            : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<LogNumberCounter> LogNumberCounters { get; set; }
    }
}
