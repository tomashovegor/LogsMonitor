using LogsMonitor.Application.Commands;
using LogsMonitor.Application.Tests.Mocks;
using LogsMonitor.Entities;
using LogsMonitor.Infrastructure.Interfaces.DataAccess;
using Moq;
using Xunit;

namespace LogsMonitor.Application.Tests.Commands
{
    public class RemoveProjectCommandTests
    {
        private readonly ProjectRepositoryMockFactory _projectRepositoryMockFactory;
        private readonly Mock<IRepository<Project>> _projectRepositoryMock;

        public RemoveProjectCommandTests()
        {
            _projectRepositoryMockFactory = new ProjectRepositoryMockFactory();
            _projectRepositoryMock = _projectRepositoryMockFactory.CreateProjectRepositoryMock();
        }

        [Fact]
        public async void RemoveProject_ProjectRemoved()
        {
            // Arrange
            RemoveProjectCommandHandler handler = new RemoveProjectCommandHandler(_projectRepositoryMock.Object);
            RemoveProjectCommand command = new RemoveProjectCommand()
            {
                ProjectId = ProjectRepositoryMockFactory.ProjectToDeleteId
            };

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.DoesNotContain(_projectRepositoryMockFactory.Projects, p => p.Id == ProjectRepositoryMockFactory.ProjectToDeleteId);
        }

        [Fact]
        public async void RemoveNonExistentProject_ThrowsNullReferenceException()
        {
            // Arrange
            RemoveProjectCommandHandler handler = new RemoveProjectCommandHandler(_projectRepositoryMock.Object);
            RemoveProjectCommand command = new RemoveProjectCommand()
            {
                ProjectId = Guid.NewGuid()
            };

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
