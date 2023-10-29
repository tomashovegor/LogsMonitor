using LogMonitor.DomainServices.Interfaces;
using LogsMonitor.Entities;

namespace LogMonitor.DomainServices.Implementation
{
    public class LogNumberCounterService : ILogNumberCounterService
    {
        public void MoveNext(LogNumberCounter logNumberCounter)
        {
            logNumberCounter.Current++;
        }
    }
}
