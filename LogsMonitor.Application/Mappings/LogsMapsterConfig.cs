using LogsMonitor.Application.DTOs;
using LogsMonitor.Entities;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace LogsMonitor.Application.Mappings
{
    public static class LogsMapsterConfig
    {
        public static void AddLogsMapsterConfig(this IServiceCollection services)
        {
            TypeAdapterConfig<CreateLogDTO, Log>
                .NewConfig()
                .Map(dest => dest.OccurrenceDate, src => DateTime.Now);
        }
    }
}
