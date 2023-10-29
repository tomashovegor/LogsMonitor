using LogsMonitor.Application.DTOs;
using LogsMonitor.Entities;
using LogsMonitor.Infrastructure.Interfaces.DataAccess;
using Mapster;
using MediatR;

namespace LogsMonitor.Application.Queries
{
    public class GetLogsQuery : IRequest<IEnumerable<LogDTO>>
    {
        public Guid ProjectId { get; set; }
        public int Skip { get; set; }
        public int Count { get; set; }
    }

    public class GetLogsQueryHandler : IRequestHandler<GetLogsQuery, IEnumerable<LogDTO>>
    {
        private readonly IRepository<Log> _logRepository;

        public GetLogsQueryHandler(IRepository<Log> logRepository)
        {
            _logRepository = logRepository;
        }

        public Task<IEnumerable<LogDTO>> Handle(GetLogsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Log> logs = _logRepository.GetAll()
                                                 .Where(l => l.ProjectId == request.ProjectId)
                                                 .Skip(request.Skip)
                                                 .Take(request.Count)
                                                 .AsEnumerable();

            IEnumerable<LogDTO> logsDTOs = logs.Adapt<IEnumerable<LogDTO>>();

            return Task.FromResult(logsDTOs);
        }
    }
}
