using LogsMonitor.Application.DTOs;
using System.Net;

namespace LogsMonitor.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError($"Message: {exception.Message}{Environment.NewLine}Trace: {exception.StackTrace}");

            ExceptionDTO exceptionResponse = exception switch
            {
                NullReferenceException => new ExceptionDTO(HttpStatusCode.NotFound, exception.Message),
                _ => new ExceptionDTO(HttpStatusCode.BadRequest, exception.Message)
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)exceptionResponse.StatusCode;

            await context.Response.WriteAsJsonAsync(exceptionResponse);
        }
    }

    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static void UseExceptionHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
