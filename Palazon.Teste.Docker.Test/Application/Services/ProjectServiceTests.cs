using Xunit;
using Moq;
using Palazon.Teste.Docker.Application.Interfaces;
using Palazon.Teste.Docker.Application.Services;
using Palazon.Teste.Docker.Domain.Interfaces;
using Palazon.Teste.Docker.Application.DTO;
using Palazon.Teste.Docker.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Palazon.Teste.Docker.Test.Application.Services
{
    public class ProjectServiceTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly ProjectService _projectService;

        public ProjectServiceTests()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _projectService = new ProjectService(_projectRepositoryMock.Object);
        }

        [Fact]
        public async Task Should_Create_Project_Successfully()
        {
            // Arrange
            var projectDto = new ProjectDTO
            {
                Name = "New Project",
                UserId = 1
            };

            // Act
            await _projectService.CreateProjectAsync(projectDto);

            // Assert
            _projectRepositoryMock.Verify(repo => repo.AddProjectAsync(It.IsAny<Project>()), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Deleting_Project_With_Pending_Tasks()
        {
            // Arrange
            var project = new Project
            {
                Tasks = new List<Tasks>
                {
                    new Tasks { Status = "Pending" }
                }
            };

            _projectRepositoryMock
                .Setup(repo => repo.GetProjectByIdAsync(1))
                .ReturnsAsync(project);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _projectService.DeleteProjectAsync(1));
        }
    }
}
