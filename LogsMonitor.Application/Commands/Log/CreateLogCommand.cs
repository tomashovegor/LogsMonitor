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
        private readonly IRepository<LogNumberCounter> _logNumberCounterRepository;
        private readonly ILogNumberService _logNumberService;
        private readonly ILogNumberCounterService _logNumberCounterService;

        public CreateLogCommandHandler(IRepository<Log> logRepository, IRepository<LogNumberCounter> logNumberCounterRepository,
            ILogNumberCounterService logNumberCounterService, ILogNumberService logNumberService)
        {
            _logRepository = logRepository;
            _logNumberCounterRepository = logNumberCounterRepository;
            _logNumberService = logNumberService;
            _logNumberCounterService = logNumberCounterService;
        }

        public async Task Handle(CreateLogCommand request, CancellationToken cancellationToken)
        {
            Log log = request.CreateLogDTO.Adapt<Log>();
            LogNumberCounter logNumberCounter = await _logNumberCounterRepository.GetById(request.CreateLogDTO.ProjectId);

            _logNumberCounterService.MoveNext(logNumberCounter);

            log.Number = _logNumberService.GetLogNumber(logNumberCounter.Prefix, logNumberCounter.Current);

            await _logNumberCounterRepository.Update(logNumberCounter);
            await _logRepository.Add(log);
        }
    }
}
