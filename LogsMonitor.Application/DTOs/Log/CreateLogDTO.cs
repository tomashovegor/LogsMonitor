namespace LogsMonitor.Application.DTOs
{
    public class CreateLogDTO
    {
        public string Message { get; set; } = null!;
        public string Trace { get; set; } = null!;
        public Guid ProjectId { get; set; }
    }
}
