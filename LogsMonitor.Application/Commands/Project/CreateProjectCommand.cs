using LogsMonitor.Application.DTOs;
using LogsMonitor.Entities;
using LogsMonitor.Infrastructure.Interfaces.DataAccess;
using Mapster;
using MediatR;

namespace LogsMonitor.Application.Commands
{
    public class CreateProjectCommand : IRequest
    {
        public CreateProjectDTO CreateProjectDTO { get; set; } = null!;
    }

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand>
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IMediator _mediator;

        public CreateProjectCommandHandler(IRepository<Project> projectRepository, IMediator mediator)
        {
            _projectRepository = projectRepository;
            _mediator = mediator;
        }

        public async Task Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            Project project = request.CreateProjectDTO.Adapt<Project>();

            Guid projectId = await _projectRepository.Add(project);

            await _mediator.Send(new CreateLogNumberCounterCommand()
            {
                CreateLogNumberCounterDTO = new CreateLogNumberCounterDTO()
                {
                    ProjectId = projectId,
                    Prefix = request.CreateProjectDTO.Prefix
                }
            });
        }
    }
}