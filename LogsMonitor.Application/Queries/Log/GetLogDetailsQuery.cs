using LogsMonitor.Application.DTOs;
using LogsMonitor.Entities;
using LogsMonitor.Infrastructure.Interfaces.DataAccess;
using Mapster;
using MediatR;

namespace LogsMonitor.Application.Queries
{
    public class GetLogDetailsQuery : IRequest<LogDetailsDTO>
    {
        public Guid LogId { get; set; }
    }

    public class GetLogDetailsQueryHandler : IRequestHandler<GetLogDetailsQuery, LogDetailsDTO>
    {
        private readonly IRepository<Log> _logRepository;

        public GetLogDetailsQueryHandler(IRepository<Log> logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<LogDetailsDTO> Handle(GetLogDetailsQuery request, CancellationToken cancellationToken)
        {
            Log log = await _logRepository.GetById(request.LogId);

            LogDetailsDTO logDetailsDTO = log.Adapt<LogDetailsDTO>();

            return logDetailsDTO;
        }
    }
}
