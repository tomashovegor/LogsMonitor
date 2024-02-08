using LogMonitor.DomainServices.Implementation.Extensions;
using LogsMonitor.Application.Extensions;
using LogsMonitor.DataAccess.MSSQL.Extensions;
using LogsMonitor.Middlewares;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Serilog;

namespace LogsMonitor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Settings"));
            builder.Configuration.AddJsonFile($"settings.connections.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", false);
            builder.Configuration.AddJsonFile($"settings.serilog.json", false);

            builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddApiVersioning().AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";
                o.SubstituteApiVersionInUrl = true;
            });
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Logs Monitor API",
                    Contact = new OpenApiContact
                    {
                        Name = "GitHub",
                        Url = new Uri("https://github.com/tomashovegor/LogsMonitor")
                    }
                });

                string apiXmlDocFilePath = Path.Combine(AppContext.BaseDirectory, "LogsMonitor.Controllers.xml");
                options.IncludeXmlComments(apiXmlDocFilePath);
            });

            builder.Services.AddDomainServicesModule()
                            .AddMSSQLDALModule(builder.Configuration)
                            .AddApplicationModule();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                IApiDescriptionGroupCollectionProvider groupProvider = app.Services.GetRequiredService<IApiDescriptionGroupCollectionProvider>();
                foreach (ApiDescriptionGroup groupDescription in groupProvider.ApiDescriptionGroups.Items.OrderBy(x => x.GroupName))
                {
                    string url = $"/swagger/{groupDescription.GroupName}/swagger.json";
                    string name = groupDescription.GroupName;

                    options.SwaggerEndpoint(url, name);
                }
            });

            app.UseExceptionHandlingMiddleware();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}