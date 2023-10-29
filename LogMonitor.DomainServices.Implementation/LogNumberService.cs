using LogMonitor.DomainServices.Interfaces;

namespace LogMonitor.DomainServices.Implementation
{
    public class LogNumberService : ILogNumberService
    {
        public string GetLogNumber(string prefix, int number)
        {
            if (string.IsNullOrEmpty(prefix))
            {
                throw new ArgumentNullException(nameof(prefix), "Передан пустой префикс номера");
            }

            return $"{prefix}-{number}";
        }
    }
}
