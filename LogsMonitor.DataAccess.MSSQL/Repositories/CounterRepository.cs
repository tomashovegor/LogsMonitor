using LogsMonitor.Entities.Base;
using LogsMonitor.Infrastructure.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace LogsMonitor.DataAccess.MSSQL.Repositories
{
    public class CounterRepository<T> : ICounterRepository<T> where T : CounterBase
    {
        private readonly DBContext _context;
        private readonly DbSet<T> _entitySet;

        public CounterRepository(DBContext context)
        {
            _context = context;
            _entitySet = context.Set<T>();
        }

        public async Task MoveNext(T counter)
        {
            await _entitySet.Where(c => c.Id == counter.Id)
                            .ExecuteUpdateAsync(b =>
                                b.SetProperty(c => c.Current, counter.Current)
                            );
        }
    }
}
