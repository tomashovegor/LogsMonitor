using LogsMonitor.Entities.Base;

namespace LogsMonitor.Entities
{
    public class ErrorLog : EntityBase
    {
        public string Number { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string Trace { get; set; } = null!;
        public DateTime OccurrenceDate { get; set; }
        public Guid ProjectId { get; set; }
    }
}
