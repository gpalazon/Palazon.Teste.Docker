using Microsoft.EntityFrameworkCore;
using Palazon.Teste.Docker.Domain.Interfaces;
using Palazon.Teste.Docker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palazon.Teste.Docker.Infraestructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context) => _context = context;


        public async Task<Project> GetProjectByIdAsync(int id)
        {
            return await _context.Projects.Where(p => p.Id == id)
                .Include(p => p.Tasks).FirstOrDefaultAsync();
        }

        public async Task<List<Project>> GetAllProjectsAsync() => await _context.Projects.Include(p => p.Tasks).ToListAsync();

        public async Task<List<Project>> GetAllProjectsByUserAsync(int userId)
        {
            return await _context.Projects.Where(x => x.UserId == userId)
                .Include(p => p.Tasks).ToListAsync();
        }


        public async Task AddProjectAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(Project project)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }

    }
}
