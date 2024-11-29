using Xunit;
using Moq;
using Palazon.Teste.Docker.Application.Interfaces;
using Palazon.Teste.Docker.Application.Services;
using Palazon.Teste.Docker.Domain.Interfaces;
using Palazon.Teste.Docker.Domain.Models;
using Palazon.Teste.Docker.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Palazon.Teste.Docker.Test.Application.Services
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly Mock<ITaskHistoryRepository> _historyRepositoryMock;
        private readonly ITaskService _taskService;

        public TaskServiceTests()
        {
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _historyRepositoryMock = new Mock<ITaskHistoryRepository>();
            _taskService = new TaskService(_taskRepositoryMock.Object, _historyRepositoryMock.Object);
        }

        [Fact]
        public async Task Should_Add_Task_Successfully()
        {


            // Arrange
            var taskDto = new TaskDTO
            {
                Title = "New Task",
                Description = "Task Description",
                Priority = "Média",
                ProjectId = 1
            };

            _taskRepositoryMock
                .Setup(repo => repo.GetTasksByProjectIdAsync(taskDto.ProjectId))
                .ReturnsAsync(new List<Tasks>());

            // Act
            await _taskService.AddTaskAsync(taskDto);

            // Assert
            _taskRepositoryMock.Verify(repo => repo.AddTaskAsync(It.IsAny<Tasks>()), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Task_Limit_Reached()
        {
            // Arrange
            var taskDto = new TaskDTO { ProjectId = 1 };
            var existingTasks = new List<Tasks>(new Tasks[20]);

            _taskRepositoryMock
                .Setup(repo => repo.GetTasksByProjectIdAsync(taskDto.ProjectId))
                .ReturnsAsync(existingTasks);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _taskService.AddTaskAsync(taskDto));
        }
    }
}
