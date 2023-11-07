namespace LogsMonitor.Application.DTOs
{
    public class LogNumberCounterDTO
    {
        public Guid Id { get; set; }
        public int Current { get; set; }
        public string Prefix { get; set; } = null!;
    }
}
