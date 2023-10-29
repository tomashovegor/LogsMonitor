using LogsMonitor.Application.DTOs;
using LogsMonitor.Entities;
using LogsMonitor.Infrastructure.Interfaces.DataAccess;
using Mapster;
using MediatR;

namespace LogsMonitor.Application.Commands
{
    public class UpdateProjectCommand : IRequest
    {
        public UpdateProjectDTO UpdateProjectDTO { get; set; } = null!;
    }

    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand>
    {
        private readonly IRepository<Project> _projectRepository;

        public UpdateProjectCommandHandler(IRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            Project project = request.UpdateProjectDTO.Adapt<Project>();

            await _projectRepository.Update(project);
        }
    }
}
