using LogsMonitor.Application.Commands;
using LogsMonitor.Application.DTOs;
using LogsMonitor.Application.Tests.Mocks;
using LogsMonitor.Entities;
using LogsMonitor.Infrastructure.Interfaces.DataAccess;
using MediatR;
using Moq;
using Xunit;

namespace LogsMonitor.Application.Tests.Commands
{
    public class CreateProjectCommandTests
    {
        private readonly ProjectRepositoryMockFactory _projectRepositoryMockFactory;
        private readonly Mock<IRepository<Project>> _projectRepositoryMock;
        private readonly Mock<IMediator> _mediatorMock;

        public CreateProjectCommandTests()
        {
            _projectRepositoryMockFactory = new ProjectRepositoryMockFactory();
            _projectRepositoryMock = _projectRepositoryMockFactory.CreateProjectRepositoryMock();
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async void CreateProject_ProjectCreated()
        {
            // Arrange
            CreateProjectCommandHandler handler = new CreateProjectCommandHandler(_projectRepositoryMock.Object, _mediatorMock.Object);
            CreateProjectCommand command = new CreateProjectCommand()
            {
                CreateProjectDTO = new CreateProjectDTO()
                {
                    Name = "Test",
                    Prefix = "T"
                }
            };

            // Act
            Guid projectId = await handler.Handle(command, CancellationToken.None);

            // Assert
            Project project = _projectRepositoryMockFactory.Projects.Where(p => p.Id == projectId).FirstOrDefault();

            Assert.NotNull(project);
            Assert.Equal(projectId, project.Id);
            Assert.Equal(command.CreateProjectDTO.Name, project.Name);
        }
    }
}
