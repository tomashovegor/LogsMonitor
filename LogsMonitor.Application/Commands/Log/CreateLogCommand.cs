using LogMonitor.DomainServices.Interfaces;
using LogsMonitor.Application.DTOs;
using LogsMonitor.Entities;
using LogsMonitor.Infrastructure.Interfaces.DataAccess;
using Mapster;
using MediatR;

namespace LogsMonitor.Application.Commands
{
    public class CreateLogCommand : IRequest
    {
        public CreateLogDTO CreateLogDTO { get; set; } = null!;
    }

    public class CreateLogCommandHandler : IRequestHandler<CreateLogCommand>
    {
        private readonly IRepository<Log> _logRepository;
        private readonly ILogNumberService _logNumberService;

        public CreateLogCommandHandler(IRepository<Log> logRepository, ILogNumberService logNumberService)
        {
            _logRepository = logRepository;
            _logNumberService = logNumberService;
        }

        public async Task Handle(CreateLogCommand request, CancellationToken cancellationToken)
        {
            Log log = request.CreateLogDTO.Adapt<Log>();

            log.Number = _logNumberService.GetLogNumber("test", 0);

            await _logRepository.Add(log);
        }
    }
}
