using LogsMonitor.Application.DTOs;
using LogsMonitor.Entities;
using LogsMonitor.Infrastructure.Interfaces.DataAccess;
using Mapster;
using MediatR;

namespace LogsMonitor.Application.Queries
{
    public class GetProjectDetailsQuery : IRequest<ProjectDetailsDTO>
    {
        public Guid ProjectId { get; set; }
    }

    public class GetProjectDetailsQueryHandler : IRequestHandler<GetProjectDetailsQuery, ProjectDetailsDTO>
    {
        private readonly IRepository<Project> _projectRepository;

        public GetProjectDetailsQueryHandler(IRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async  Task<ProjectDetailsDTO> Handle(GetProjectDetailsQuery request, CancellationToken cancellationToken)
        {
            Project project = await _projectRepository.GetById(request.ProjectId);

            if (project == null)
            {
                throw new NullReferenceException($"Проект с Id: {request.ProjectId} не найден");
            }

            ProjectDetailsDTO projectDetailsDTO = project.Adapt<ProjectDetailsDTO>();

            return projectDetailsDTO;
        }
    }
}
