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
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context) => _context = context;


        public async Task<Tasks> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Tasks>> GetTasksByProjectIdAsync(int projectId)
        {
            return await _context.Tasks.Where(x => x.ProjectId == projectId).Include(x => x.User).ToListAsync();
        }

        public async Task AddTaskAsync(Tasks task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(Tasks task)
        {
            var existingTask = await _context.Tasks.FindAsync(task.Id);
            if (existingTask == null)
            {
                throw new KeyNotFoundException($"Tarefa com id {task.Id} não encontrada.");
            }

            // Atualiza as propriedades do usuário
            existingTask.Description = task.Description;
            existingTask.Status = task.Status;
            existingTask.DueDate = task.DueDate;
            existingTask.Title = task.Title;
            existingTask.DueDate = task.DueDate;

            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(Tasks task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }


    }
}
