using LogsMonitor.Entities.Base;
using LogsMonitor.Infrastructure.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace LogsMonitor.DataAccess.PostgreSQL.Repositories
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

        public IQueryable<T> GetAll()
        {
            IQueryable<T> entities = _entitySet.AsNoTracking();

            return entities;
        }

        public async Task<Guid> Add(T entity)
        {
            EntityEntry<T> entityEntry = _entitySet.Add(entity);

            await _context.SaveChangesAsync();

            return entityEntry.Entity.Id;
        }

        public async Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task Remove(Guid entityId)
        {
            await _entitySet.Where(e => e.Id == entityId)
                            .ExecuteDeleteAsync();
        }

        public async Task<bool> Exists(Guid entityId)
        {
            return await _entitySet.AnyAsync(e => e.Id == entityId);
        }
    }
}
