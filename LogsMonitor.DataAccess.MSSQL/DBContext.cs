using LogsMonitor.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LogsMonitor.DataAccess.MSSQL
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) 
            : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
