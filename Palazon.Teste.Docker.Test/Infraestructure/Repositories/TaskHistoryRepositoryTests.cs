using Xunit;
using Microsoft.EntityFrameworkCore;
using Palazon.Teste.Docker.Domain.Models;
using Palazon.Teste.Docker.Infraestructure;
using Palazon.Teste.Docker.Infraestructure.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Palazon.Teste.Docker.Test.Infraestructure.Repositories
{
    public class TaskHistoryRepositoryTests
    {
        private readonly TaskHistoryRepository _repository;
        private readonly AppDbContext _context;

        public TaskHistoryRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TaskHistoryRepositoryTests")
                .Options;

            _context = new AppDbContext(options);
            _repository = new TaskHistoryRepository(_context);
        }

        [Fact]
        public async Task Should_Add_Task_History()
        {
            // Arrange
            var history = new TaskHistory
            {
                TaskId = 1,
                PropertyName = "Title",
                NewValue = "New Task",
                ModifiedById = 1
            };

            // Act
            await _repository.AddTaskHistoryAsync(history.TaskId, history.PropertyName, string.Empty, history.NewValue, history.ModifiedById);
            var result = await _context.TaskHistories.FirstOrDefaultAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Title", result.PropertyName);
        }

        [Fact]
        public async Task Should_Get_Histories_By_TaskId()
        {
            // Arrange
            _context.TaskHistories.AddRange(
                new TaskHistory { TaskId = 1, PropertyName = "Title", NewValue = "Updated Task" },
                new TaskHistory { TaskId = 2, PropertyName = "Description", NewValue = "Updated Description" }
            );
            await _context.SaveChangesAsync();

            // Act
            var histories = await _repository.GetTaskHistoriesAsync(1);

            // Assert
            Assert.Single(histories);
            Assert.Equal("Title", histories.First().PropertyName);
        }
    }
}
