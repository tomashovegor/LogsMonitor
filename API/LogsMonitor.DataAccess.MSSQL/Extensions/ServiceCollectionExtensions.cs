using LogsMonitor.DataAccess.MSSQL.Repositories;
using LogsMonitor.Infrastructure.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LogsMonitor.DataAccess.MSSQL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMSSQLDALModule(this IServiceCollection services, IConfiguration configuration)
        {
            string MSSQLConnectionString = configuration.GetConnectionString("DefaultConnectionString");
            services.AddDbContext<DBContext>(options => options.UseSqlServer(MSSQLConnectionString));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(ICounterRepository<>), typeof(CounterRepository<>));

            return services;
        }
    }
}
