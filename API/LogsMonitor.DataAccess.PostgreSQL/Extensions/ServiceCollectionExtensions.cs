using LogsMonitor.DataAccess.PostgreSQL.Repositories;
using LogsMonitor.Infrastructure.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LogsMonitor.DataAccess.PostgreSQL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPostgreSQLDALModule(this IServiceCollection services, IConfiguration configuration)
        {
            string DBConnectionString = configuration.GetConnectionString("DefaultConnectionString");
            services.AddDbContext<DBContext>(options => options.UseNpgsql(DBConnectionString));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(ICounterRepository<>), typeof(CounterRepository<>));

            return services;
        }
    }
}
