using LogMonitor.DomainServices.Interfaces;

namespace LogMonitor.DomainServices.Implementation
{
    public class LogNumberService : ILogNumberService
    {
        public string GetLogNumber(string projectCode, int number)
        {
            if (string.IsNullOrEmpty(projectCode))
            {
                throw new ArgumentNullException(nameof(projectCode), "Передан пустой код проекта");
            }

            return $"{projectCode}-{number}";
        }
    }
}
