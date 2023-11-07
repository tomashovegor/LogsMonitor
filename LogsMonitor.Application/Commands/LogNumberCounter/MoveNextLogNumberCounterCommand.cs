using LogsMonitor.Application.DTOs;
using LogsMonitor.Entities;
using LogsMonitor.Infrastructure.Interfaces.DataAccess;
using Mapster;
using MediatR;

namespace LogsMonitor.Application.Commands
{
    public class MoveNextLogNumberCounterCommand : IRequest
    {
        public MoveNextLogNumberCounterDTO MoveNextLogNumberCounterDTO { get; set; } = null!;
    }

    public class MoveNextLogNumberCounterCommandHandler : IRequestHandler<MoveNextLogNumberCounterCommand>
    {
        private readonly ICounterRepository<LogNumberCounter> _counterRepository;

        public MoveNextLogNumberCounterCommandHandler(ICounterRepository<LogNumberCounter> counterRepository)
        {
            _counterRepository = counterRepository;
        }

        public async Task Handle(MoveNextLogNumberCounterCommand request, CancellationToken cancellationToken)
        {
            LogNumberCounter logNumberCounter = request.MoveNextLogNumberCounterDTO.Adapt<LogNumberCounter>();

            logNumberCounter.Current++;

            await _counterRepository.MoveNext(logNumberCounter);
        }
    }
}
