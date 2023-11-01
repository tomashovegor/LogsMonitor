namespace LogsMonitor.Application.DTOs
{
    public class LogDetailsDTO
    {
        public Guid Id { get; set; }
        public string Number { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string Trace { get; set; } = null!;
        public DateTime OccurrenceDate { get; set; }
    }
}
