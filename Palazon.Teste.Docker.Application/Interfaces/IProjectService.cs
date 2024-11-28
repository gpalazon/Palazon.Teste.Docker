using Palazon.Teste.Docker.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palazon.Teste.Docker.Application.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectDTO>> GetAllProjectsAsync(int userId);
        Task<ProjectDTO> GetProjectByIdAsync(int id);
        Task CreateProjectAsync(ProjectDTO project);
        Task DeleteProjectAsync(int id);
    }
}
