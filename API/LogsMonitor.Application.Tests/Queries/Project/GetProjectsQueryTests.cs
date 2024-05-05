using LogsMonitor.Application.DTOs;
using LogsMonitor.Application.Queries;
using LogsMonitor.Application.Tests.Mocks;
using LogsMonitor.Entities;
using LogsMonitor.Infrastructure.Interfaces.DataAccess;
using Moq;
using Xunit;

namespace LogsMonitor.Application.Tests.Queries
{
    public class GetProjectsQueryTests
    {
        private readonly Mock<IRepository<Project>> _projectRepositoryMock;

        public GetProjectsQueryTests() 
        {
            _projectRepositoryMock = new ProjectRepositoryMockFactory().CreateProjectRepositoryMock();
        }

        [Fact]
        public async void GetAllProjects_ReturnsAllProjects()
        {
            // Arrange
            GetProjectsQueryHandler handler = new GetProjectsQueryHandler(_projectRepositoryMock.Object);
            GetProjectsQuery query = new GetProjectsQuery();

            // Act
            IEnumerable<ProjectDTO> projects = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(3, projects.Count());
            Assert.Contains(projects, p => p.Id == ProjectRepositoryMockFactory.ProjectToRetrieveId);
            Assert.Contains(projects, p => p.Id == ProjectRepositoryMockFactory.ProjectToUpdateId);
            Assert.Contains(projects, p => p.Id == ProjectRepositoryMockFactory.ProjectToDeleteId);
        }
    }
}
