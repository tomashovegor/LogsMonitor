using LogsMonitor.Application.Commands;
using LogsMonitor.Application.DTOs;
using LogsMonitor.Application.Tests.Mocks;
using LogsMonitor.Entities;
using LogsMonitor.Infrastructure.Interfaces.DataAccess;
using MediatR;
using Moq;
using System.Reflection;
using Xunit;

namespace LogsMonitor.Application.Tests.Commands
{
    public class UpdateProjectCommandTests
    {
        private readonly ProjectRepositoryMockFactory _projectRepositoryMockFactory;
        private readonly Mock<IRepository<Project>> _projectRepositoryMock;

        public UpdateProjectCommandTests()
        {
            _projectRepositoryMockFactory = new ProjectRepositoryMockFactory();
            _projectRepositoryMock = _projectRepositoryMockFactory.CreateProjectRepositoryMock();
        }

        [Fact]
        public async void UpdateProject_ProjectUpdated()
        {
            // Arrange
            UpdateProjectCommandHandler handler = new UpdateProjectCommandHandler(_projectRepositoryMock.Object);
            UpdateProjectCommand command = new UpdateProjectCommand()
            {
                UpdateProjectDTO = new UpdateProjectDTO()
                {
                    Id = ProjectRepositoryMockFactory.ProjectToUpdateId,
                    Name = "New name"
                }
            };

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            Project project = _projectRepositoryMockFactory.Projects.Where(p => p.Id == ProjectRepositoryMockFactory.ProjectToUpdateId).FirstOrDefault();

            Assert.NotNull(project);
            Assert.Equal(ProjectRepositoryMockFactory.ProjectToUpdateId, project.Id);
            Assert.Equal(command.UpdateProjectDTO.Name, project.Name);
        }

        [Fact]
        public async void UpdateNonExistentProject_ThrowsNullReferenceException()
        {
            // Arrange
            UpdateProjectCommandHandler handler = new UpdateProjectCommandHandler(_projectRepositoryMock.Object);
            UpdateProjectCommand command = new UpdateProjectCommand()
            {
                UpdateProjectDTO = new UpdateProjectDTO()
                {
                    Id = Guid.NewGuid(),
                    Name = "New name"
                }
            };

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
