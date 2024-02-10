using LogsMonitor.Entities;
using LogsMonitor.Infrastructure.Interfaces.DataAccess;
using Moq;

namespace LogsMonitor.Application.Tests.Mocks
{
    public class ProjectRepositoryMockFactory
    {
        public static Guid ProjectToRetrieveId = Guid.NewGuid();
        public static Guid ProjectToUpdateId = Guid.NewGuid();
        public static Guid ProjectToDeleteId = Guid.NewGuid();

        public List<Project> Projects = new List<Project>()
        {
            new Project()
            {
                Id = ProjectToRetrieveId,
                Name = "Project to retrieve"
            },
            new Project()
            {
                Id = ProjectToUpdateId,
                Name = "Project to update"
            },
            new Project()
            {
                Id = ProjectToDeleteId,
                Name = "Project to delete"
            },
        };

        public Mock<IRepository<Project>> CreateProjectRepositoryMock()
        {
            Mock<IRepository<Project>> repositoryMock = new Mock<IRepository<Project>>();

            repositoryMock.Setup(r => r.GetById(It.IsAny<Guid>()))
                          .ReturnsAsync((Guid projectId) =>
                          {
                              Project project = Projects.Where(p => p.Id == projectId).FirstOrDefault();

                              if (project == null)
                              {
                                  throw new NullReferenceException();
                              }

                              return project;
                          });

            repositoryMock.Setup(r => r.GetAll())
                          .Returns(Projects.AsQueryable());

            repositoryMock.Setup(r => r.Add(It.IsAny<Project>()))
                          .ReturnsAsync((Project project) =>
                          {
                              project.Id = Guid.NewGuid();

                              Projects.Add(project);

                              return project.Id;
                          });

            repositoryMock.Setup(r => r.Update(It.IsAny<Project>()))
                          .Callback((Project project) =>
                          {
                              Project projectToUpdate = Projects.Where(p => p.Id == project.Id).FirstOrDefault();

                              projectToUpdate.Id = project.Id;
                              projectToUpdate.Name = project.Name;
                          });

            repositoryMock.Setup(r => r.Remove(It.IsAny<Guid>()))
                          .Callback((Guid projectId) =>
                          {
                              Project project = Projects.Where(p => p.Id == projectId).FirstOrDefault();

                              Projects.Remove(project);
                          });

            repositoryMock.Setup(r => r.Exists(It.IsAny<Guid>()))
                          .ReturnsAsync((Guid projectId) =>
                          {
                              return Projects.Any(p => p.Id == projectId);
                          });

            return repositoryMock;
        }
    }
}
