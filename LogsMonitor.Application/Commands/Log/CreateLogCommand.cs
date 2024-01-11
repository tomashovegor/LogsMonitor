using LogMonitor.DomainServices.Interfaces;
using LogsMonitor.Application.DTOs;
using LogsMonitor.Application.Queries;
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
        private readonly IMediator _mediator;

        private static object _counterLocker;

        public CreateLogCommandHandler(IRepository<Log> logRepository, ILogNumberService logNumberService, IMediator mediator)
        {
            _logRepository = logRepository;
            _logNumberService = logNumberService;
            _mediator = mediator;
        }

        static CreateLogCommandHandler()
        {
            _counterLocker = new object();
        }

        public async Task Handle(CreateLogCommand request, CancellationToken cancellationToken)
        {
            Log log = request.CreateLogDTO.Adapt<Log>();
            log.Number = GetLogNumberAsync(request.CreateLogDTO.ProjectId);
            
            await _logRepository.Add(log);
        }

        private string GetLogNumberAsync(Guid projectId)
        {
            string logNumber = null;

            lock (_counterLocker)
            {
                LogNumberCounterDTO logNumberCounterDTO = _mediator.Send(new GetLogNumberCounterQuery() { ProjectId = projectId }).Result;

                logNumber = _logNumberService.GetLogNumber(logNumberCounterDTO.Prefix, logNumberCounterDTO.Current);

                _mediator.Send(new MoveNextLogNumberCounterCommand()
                {
                    MoveNextLogNumberCounterDTO = new MoveNextLogNumberCounterDTO()
                    {
                        Id = logNumberCounterDTO.Id,
                        Current = logNumberCounterDTO.Current,
                    }
                }).Wait();
            }

            return logNumber;
        }
    }
}
