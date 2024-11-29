using Xunit;
using Microsoft.EntityFrameworkCore;
using Palazon.Teste.Docker.Domain.Models;
using Palazon.Teste.Docker.Infraestructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Palazon.Teste.Docker.Infraestructure;

namespace Palazon.Teste.Docker.Test.Infraestructure.Repositories
{
    public class TaskRepositoryTests
    {
        private readonly TaskRepository _repository;
        private readonly AppDbContext _context;

        public TaskRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TaskRepositoryTests")
                .Options;

            _context = new AppDbContext(options);
            _repository = new TaskRepository(_context);
        }

        [Fact]
        public async Task Should_Add_Task()
        {
            // Arrange
            var task = new Tasks
            {
                Title = "Test Task",
                Description = "Task description",
                Status = "Pending",
                ProjectId = 1
            };

            // Act
            await _repository.AddTaskAsync(task);
            var result = await _context.Tasks.FirstOrDefaultAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Task", result.Title);
        }

        [Fact]
        public async Task Should_Get_Tasks_By_ProjectId()
        {
            // Arrange
            _context.Tasks.AddRange(
                new Tasks { Title = "Task 1", ProjectId = 1 },
                new Tasks { Title = "Task 2", ProjectId = 1 },
                new Tasks { Title = "Task 3", ProjectId = 2 }
            );
            await _context.SaveChangesAsync();

            // Act
            var tasks = await _repository.GetTasksByProjectIdAsync(1);

            // Assert
            Assert.Equal(2, tasks.Count);
            Assert.All(tasks, t => Assert.Equal(1, t.ProjectId));
        }
    }
}
