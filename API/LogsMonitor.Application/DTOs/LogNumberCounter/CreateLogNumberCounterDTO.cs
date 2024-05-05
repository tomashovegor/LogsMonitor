namespace LogsMonitor.Application.DTOs
{
    public class CreateLogNumberCounterDTO
    {
        public Guid ProjectId { get; set; }
        public string Prefix { get; set; } = null!;
    }
}
