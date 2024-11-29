using Xunit;
using Moq;
using Palazon.Teste.Docker.Application.Services;
using Palazon.Teste.Docker.Domain.Interfaces;
using Palazon.Teste.Docker.Application.DTO;
using Palazon.Teste.Docker.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Palazon.Teste.Docker.Test.Application.Services
{
    public class TaskHistoryServiceTests
    {
        private readonly Mock<ITaskHistoryRepository> _historyRepositoryMock;
        private readonly TaskHistoryService _historyService;

        public TaskHistoryServiceTests()
        {
            _historyRepositoryMock = new Mock<ITaskHistoryRepository>();
            _historyService = new TaskHistoryService(null, _historyRepositoryMock.Object);
        }

        [Fact]
        public async Task Should_Return_Task_Histories()
        {
            // Arrange
            var taskHistories = new List<TaskHistory>
            {
                new TaskHistory { Id = 1, PropertyName = "Title", NewValue = "New Task" }
            };

            _historyRepositoryMock
                .Setup(repo => repo.GetTaskHistoriesAsync(1))
                .ReturnsAsync(taskHistories);

            // Act
            var result = await _historyService.GetTaskHistoriesAsync(1);

            // Assert
            Assert.Single(result);
            Assert.Equal("New Task", result[0].NewValue);
        }
    }
}
