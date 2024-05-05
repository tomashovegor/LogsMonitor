namespace LogMonitor.DomainServices.Interfaces
{
    public interface ILogNumberService
    {
        string GetLogNumber(string prefix, int number);
    }
}
