using LogsMonitor.Entities;
using LogsMonitor.Infrastructure.Interfaces.DataAccess;
using MediatR;

namespace LogsMonitor.Application.Commands
{
    public class RemoveProjectCommand : IRequest
    {
        public Guid ProjectId { get; set; }
    }

    public class RemoveProjectCommandHandler : IRequestHandler<RemoveProjectCommand>
    {
        private readonly IRepository<Project> _projectRepository;

        public RemoveProjectCommandHandler(IRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task Handle(RemoveProjectCommand request, CancellationToken cancellationToken)
        {
            bool projectExists = await _projectRepository.Exists(request.ProjectId);

            if (projectExists == false)
            {
                throw new NullReferenceException($"Проект с Id: {request.ProjectId} не найден");
            }

            await _projectRepository.Remove(request.ProjectId);
        }
    }
}
