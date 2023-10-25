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
            string MSSQLConnectionString = configuration.GetConnectionString("MSSQLConnectionString");
            services.AddDbContext<DBContext>(options => options.UseSqlServer(MSSQLConnectionString));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}
