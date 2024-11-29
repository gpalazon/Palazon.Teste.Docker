using Xunit;
using Microsoft.EntityFrameworkCore;
using Palazon.Teste.Docker.Domain.Models;
using Palazon.Teste.Docker.Infraestructure.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Palazon.Teste.Docker.Infraestructure;

namespace Palazon.Teste.Docker.Test.Infraestructure.Repositories
{
    public class ProjectRepositoryTests
    {
        private readonly ProjectRepository _repository;
        private readonly AppDbContext _context;

        public ProjectRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("ProjectRepositoryTests")
                .Options;

            _context = new AppDbContext(options);
            _repository = new ProjectRepository(_context);
        }

        [Fact]
        public async Task Should_Add_Project()
        {
            // Arrange
            var project = new Project
            {
                Name = "New Project",
                UserId = 1
            };

            // Act
            await _repository.AddProjectAsync(project);
            var result = await _context.Projects.FirstOrDefaultAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Project", result.Name);
        }

        [Fact]
        public async Task Should_Get_Project_By_Id()
        {
            // Arrange
            var project = new Project
            {
                Id = 1,
                Name = "Existing Project",
                UserId = 1
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetProjectByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Existing Project", result.Name);
        }
    }
}
