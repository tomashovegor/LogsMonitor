using LogsMonitor.Entities.Base;
using LogsMonitor.Infrastructure.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace LogsMonitor.DataAccess.MSSQL
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly DBContext _context;
        private readonly DbSet<T> _entitySet;

        public Repository(DBContext context)
        {
            _context = context;
            _entitySet = context.Set<T>();
        }

        public async Task<T> GetById(Guid entityId)
        {
            T entity = await _entitySet.Where(e => e.Id == entityId).FirstOrDefaultAsync();

            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> entities = _entitySet.AsNoTracking().AsEnumerable();

            return entities;
        }

        public async Task Add(T entity)
        {
            _entitySet.Add(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task Remove(Guid entityId)
        {
            T entity = await _entitySet.Where(e => e.Id == entityId).FirstOrDefaultAsync();
            _entitySet.Remove(entity);

            await _context.SaveChangesAsync();
        }

    }
}
