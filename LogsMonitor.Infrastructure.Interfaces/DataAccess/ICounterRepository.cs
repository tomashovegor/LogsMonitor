﻿using LogsMonitor.Entities.Base;

namespace LogsMonitor.Infrastructure.Interfaces.DataAccess
{
    public interface ICounterRepository<T> where T : CounterBase
    {
        Task MoveNext(T counter);
    }
}
