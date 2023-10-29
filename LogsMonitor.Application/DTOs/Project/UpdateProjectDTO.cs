namespace LogsMonitor.Application.DTOs
{
    public class UpdateProjectDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
    }
}
