using Palazon.Teste.Docker.Application.DTO;
using Palazon.Teste.Docker.Application.Interfaces;
using Palazon.Teste.Docker.Domain.Interfaces;
using Palazon.Teste.Docker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palazon.Teste.Docker.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<List<ProjectDTO>> GetAllProjectsAsync(int userId)
        {
            try
            {
                var projects = await _projectRepository.GetAllProjectsByUserAsync(userId);

                // Mapeando para o DTO
                return projects.Select(p => new ProjectDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    UserId = p.UserId,
                    TaskCount = p.Tasks.Count
                }).ToList();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
            

        }

        public async Task<ProjectDTO> GetProjectByIdAsync(int id)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);

            // Mapeando para o DTO
            return new ProjectDTO
            {
                Id = project.Id,
                Name = project.Name,
                UserId = project.UserId,
                TaskCount = project.Tasks.Count
            };
        }

        public async Task CreateProjectAsync(ProjectDTO project)
        {

            try
            {
                await _projectRepository.AddProjectAsync(new Project
                {
                    Id = project.Id,
                    Name = project.Name,
                    UserId = project.UserId,
                });


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async Task DeleteProjectAsync(int id)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);
            if (project == null)
                throw new Exception("Projeto não encontrado.");

            if (project.Tasks.Any(t => t.Status != "Completed"))
                throw new Exception("Não é possível deletar um Projeto com tarefas pendentes. Complete as tarefas antes de tentar novamente.");

            await _projectRepository.DeleteProjectAsync(project);
        }
    }
}
