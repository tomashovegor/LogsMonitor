using LogsMonitor.Application.DTOs;
using LogsMonitor.Entities;
using LogsMonitor.Infrastructure.Interfaces.DataAccess;
using Mapster;
using MediatR;

namespace LogsMonitor.Application.Queries
{
    public class GetProjectsQuery : IRequest<IEnumerable<ProjectDTO>> { }

    public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, IEnumerable<ProjectDTO>>
    {
        private readonly IRepository<Project> _projectRepository;

        public GetProjectsQueryHandler(IRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public Task<IEnumerable<ProjectDTO>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Project> projects = _projectRepository.GetAll().AsEnumerable();

            IEnumerable<ProjectDTO> projectsDTOs = projects.Adapt<IEnumerable<ProjectDTO>>();

            return Task.FromResult(projectsDTOs);
        }
    }
}
