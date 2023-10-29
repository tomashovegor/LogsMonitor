using LogsMonitor.Application.DTOs;
using LogsMonitor.Entities;
using LogsMonitor.Infrastructure.Interfaces.DataAccess;
using Mapster;
using MediatR;

namespace LogsMonitor.Application.Queries
{
    public class GetProjectQuery : IRequest<ProjectDTO>
    {
        public Guid ProjectId { get; set; }
    }

    public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, ProjectDTO>
    {
        private readonly IRepository<Project> _projectRepository;

        public GetProjectQueryHandler(IRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async  Task<ProjectDTO> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            Project project = await _projectRepository.GetById(request.ProjectId);

            ProjectDTO projectDTO = project.Adapt<ProjectDTO>();

            return projectDTO;
        }
    }
}
