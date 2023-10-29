using LogsMonitor.Entities;

namespace LogMonitor.DomainServices.Interfaces
{
    public interface ILogNumberCounterService
    {
        public void MoveNext(LogNumberCounter logNumberCounter);
    }
}
