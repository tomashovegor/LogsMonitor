using LogsMonitor.Entities.Base;

namespace LogsMonitor.Entities
{
    public class LogNumberCounter : EntityBase
    {
        public int Current { get; set; }
        public string Prefix { get; set; } = null!;
        public Guid ProjectId { get; set; }
    }
}
