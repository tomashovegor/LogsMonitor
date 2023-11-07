using LogMonitor.DomainServices.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LogMonitor.DomainServices.Implementation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServicesModule(this IServiceCollection services)
        {
            services.AddTransient<ILogNumberService, LogNumberService>();

            return services;
        }
    }
}
