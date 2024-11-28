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
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) => _context = context;

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.Include(u => u.Profile).ToListAsync();
        }


        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.Include(u => u.Profile).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<User>> GetUsersWithCompletedTasksAsync(DateTime sinceDate)
        {
            return await _context.Users
                .Include(u => u.Tasks.Where(t => t.Status == "Completed" && t.DueDate >= sinceDate))
                .ThenInclude(t => t.Project)
                .ToListAsync();
        }



    }
}
