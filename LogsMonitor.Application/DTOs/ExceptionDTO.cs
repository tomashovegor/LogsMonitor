using System.Net;

namespace LogsMonitor.Application.DTOs
{
    public class ExceptionDTO
    {
        public ExceptionDTO(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}
