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
    public class TaskHistoryRepository : ITaskHistoryRepository
    {
        private readonly AppDbContext _context;

        public TaskHistoryRepository(AppDbContext context) => _context = context;

        public async Task<List<TaskHistory>> GetTaskHistoriesAsync(int taskId)
        {
            return await _context.TaskHistories.Where(x => x.TaskId == taskId).Include(x => x.ModifiedBy).ToListAsync();
        }

        public async Task AddTaskHistoryAsync(int taskId, string propertyName, string oldValue, string newValue, int modifiedBy)
        {
            if (oldValue == newValue) return; // Não registrar se não houve alteração

            try
            {
                var history = new TaskHistory
                {
                    TaskId = taskId,
                    PropertyName = propertyName,
                    NewValue = newValue,
                    ModifiedAt = DateTime.UtcNow,
                    ModifiedById = modifiedBy
                };

                _context.TaskHistories.Add(history);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }
    }
}
