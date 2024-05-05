using LogsMonitor.Application.DTOs;
using LogsMonitor.Application.Queries;
using LogsMonitor.Application.Tests.Mocks;
using LogsMonitor.Entities;
using LogsMonitor.Infrastructure.Interfaces.DataAccess;
using Moq;
using Xunit;

namespace LogsMonitor.Application.Tests.Queries
{
    public class GetProjectDetailsQueryTests
    {
        private readonly Mock<IRepository<Project>> _projectRepositoryMock;

        public GetProjectDetailsQueryTests()
        {
            _projectRepositoryMock = new ProjectRepositoryMockFactory().CreateProjectRepositoryMock();
        }

        [Fact]
        public async void GetProject_ReturnsRequestedProject()
        {
            // Arrange
            GetProjectDetailsQueryHandler handler = new GetProjectDetailsQueryHandler(_projectRepositoryMock.Object);
            GetProjectDetailsQuery query = new GetProjectDetailsQuery()
            {
                ProjectId = ProjectRepositoryMockFactory.ProjectToRetrieveId
            };

            // Act
            ProjectDetailsDTO project = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(project);
            Assert.Equal(ProjectRepositoryMockFactory.ProjectToRetrieveId, project.Id);
        }

        [Fact]
        public async void GetNonExistentProject_ThrowsNullReferenceException()
        {
            // Arrange
            GetProjectDetailsQueryHandler handler = new GetProjectDetailsQueryHandler(_projectRepositoryMock.Object);
            GetProjectDetailsQuery query = new GetProjectDetailsQuery()
            {
                ProjectId = Guid.NewGuid()
            };

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
