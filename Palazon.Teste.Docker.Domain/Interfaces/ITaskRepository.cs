using Palazon.Teste.Docker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palazon.Teste.Docker.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<Tasks> GetTaskByIdAsync(int id);
        Task<List<Tasks>> GetTasksByProjectIdAsync(int projectId);
        Task AddTaskAsync(Tasks task);
        Task UpdateTaskAsync(Tasks task);
        Task DeleteTaskAsync(Tasks task);
    }
}
