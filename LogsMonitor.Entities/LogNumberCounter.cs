using LogsMonitor.Entities.Base;

namespace LogsMonitor.Entities
{
    public class LogNumberCounter : CounterBase
    {
        public string Prefix { get; set; } = null!;
        public Guid ProjectId { get; set; }
    }
}
