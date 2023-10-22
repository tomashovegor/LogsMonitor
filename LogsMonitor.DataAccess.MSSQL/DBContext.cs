using LogsMonitor.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LogsMonitor.DataAccess.MSSQL
{
    public class DBContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string SQLConnectionString = _configuration.GetConnectionString("MSSQLConnection");
            optionsBuilder.UseSqlServer(SQLConnectionString);
        }
    }
}
