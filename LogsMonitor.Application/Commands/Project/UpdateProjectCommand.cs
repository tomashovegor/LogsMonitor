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
            bool projectExists = await _projectRepository.Exists(request.UpdateProjectDTO.Id);

            if (projectExists == false)
            {
                throw new NullReferenceException($"Проект с Id: {request.UpdateProjectDTO.Id} не найден");
            }

            Project project = request.UpdateProjectDTO.Adapt<Project>();

            await _projectRepository.Update(project);
        }
    }
}
