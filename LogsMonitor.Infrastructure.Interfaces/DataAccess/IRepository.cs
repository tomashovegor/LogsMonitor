using LogsMonitor.Entities.Base;

namespace LogsMonitor.Infrastructure.Interfaces.DataAccess
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<T> GetById(Guid entityId);
        IQueryable<T> GetAll();
        Task Add(T entity);
        Task Update(T entity);
        Task Remove(Guid entityId);
    }
}
