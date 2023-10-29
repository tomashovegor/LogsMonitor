using LogsMonitor.Application.DTOs;
using LogsMonitor.Entities;
using LogsMonitor.Infrastructure.Interfaces.DataAccess;
using Mapster;
using MediatR;

namespace LogsMonitor.Application.Queries
{
    public class GetLogQuery : IRequest<LogDTO>
    {
        public Guid LogId { get; set; }
    }

    public class GetLogQueryHandler : IRequestHandler<GetLogQuery, LogDTO>
    {
        private readonly IRepository<Log> _logRepository;

        public GetLogQueryHandler(IRepository<Log> logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<LogDTO> Handle(GetLogQuery request, CancellationToken cancellationToken)
        {
            Log log = await _logRepository.GetById(request.LogId);

            LogDTO logDTO = log.Adapt<LogDTO>();

            return logDTO;
        }
    }
}
