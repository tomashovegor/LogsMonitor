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

        public CreateProjectCommandHandler(IRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            Project project = request.CreateProjectDTO.Adapt<Project>();

            await _projectRepository.Add(project);
        }
    }
}