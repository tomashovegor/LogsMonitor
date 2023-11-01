using LogMonitor.DomainServices.Implementation.Extensions;
using LogsMonitor.Application.Extensions;
using LogsMonitor.DataAccess.MSSQL.Extensions;

namespace LogsMonitor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Configuration.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Settings"));
            builder.Configuration.AddJsonFile($"settings.connections.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", false);

            builder.Services.AddDomainServicesModule()
                            .AddMSSQLDALModule(builder.Configuration)
                            .AddApplicationModule();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}