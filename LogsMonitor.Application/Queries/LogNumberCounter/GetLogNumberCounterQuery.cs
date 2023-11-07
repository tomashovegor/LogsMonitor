using LogsMonitor.Application.DTOs;
using LogsMonitor.Entities;
using LogsMonitor.Infrastructure.Interfaces.DataAccess;
using Mapster;
using MediatR;

namespace LogsMonitor.Application.Queries
{
    public class GetLogNumberCounterQuery : IRequest<LogNumberCounterDTO>
    {
        public Guid ProjectId { get; set; }
    }

    public class GetLogNumberCounterQueryHandler : IRequestHandler<GetLogNumberCounterQuery, LogNumberCounterDTO>
    {
        private readonly IRepository<LogNumberCounter> _logNumberCounterRepository;

        public GetLogNumberCounterQueryHandler(IRepository<LogNumberCounter> logNumberCounterRepository)
        {
            _logNumberCounterRepository = logNumberCounterRepository;
        }

        public Task<LogNumberCounterDTO> Handle(GetLogNumberCounterQuery request, CancellationToken cancellationToken)
        {
            LogNumberCounter logNumberCounter = _logNumberCounterRepository.GetAll()
                                                                           .Where(lnc => lnc.ProjectId == request.ProjectId)
                                                                           .FirstOrDefault();

            LogNumberCounterDTO logNumberCounterDTO = logNumberCounter.Adapt<LogNumberCounterDTO>();

            return Task.FromResult(logNumberCounterDTO);
        }
    }
}
