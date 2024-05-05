using LogsMonitor.Application.DTOs;
using LogsMonitor.Entities;
using LogsMonitor.Infrastructure.Interfaces.DataAccess;
using Mapster;
using MediatR;

namespace LogsMonitor.Application.Commands
{
    public class CreateLogNumberCounterCommand : IRequest
    {
        public CreateLogNumberCounterDTO CreateLogNumberCounterDTO { get; set; } = null!;
    }

    public class CreateLogNumberCounterCommandHandler : IRequestHandler<CreateLogNumberCounterCommand>
    {
        private readonly IRepository<LogNumberCounter> _logNumberCounterRepository;

        public CreateLogNumberCounterCommandHandler(IRepository<LogNumberCounter> logNumberCounterRepository)
        {
            _logNumberCounterRepository = logNumberCounterRepository;
        }

        public async Task Handle(CreateLogNumberCounterCommand request, CancellationToken cancellationToken)
        {
            LogNumberCounter logNumberCounter = request.CreateLogNumberCounterDTO.Adapt<LogNumberCounter>();
            logNumberCounter.Current = 1;

            await _logNumberCounterRepository.Add(logNumberCounter);
        }
    }
}
