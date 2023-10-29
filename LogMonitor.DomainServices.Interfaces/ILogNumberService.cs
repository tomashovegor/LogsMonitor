namespace LogMonitor.DomainServices.Interfaces
{
    public interface ILogNumberService
    {
        string GetLogNumber(string projectCode, int number);
    }
}
