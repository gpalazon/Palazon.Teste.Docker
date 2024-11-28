using Palazon.Teste.Docker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palazon.Teste.Docker.Domain.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project> GetProjectByIdAsync(int id);
        Task<List<Project>> GetAllProjectsAsync();
        Task<List<Project>> GetAllProjectsByUserAsync(int userId);
        Task AddProjectAsync(Project project);
        Task DeleteProjectAsync(Project project);
    }
}
