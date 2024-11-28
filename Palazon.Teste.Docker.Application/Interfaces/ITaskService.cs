using Palazon.Teste.Docker.Application.DTO;
using Palazon.Teste.Docker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palazon.Teste.Docker.Application.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskDTO>> GetTasksByProjectIdAsync(int projectId);
        Task AddTaskAsync(TaskDTO task);
        Task UpdateTaskAsync(TaskDTO task, int userId);
        Task DeleteTaskAsync(int id);
        Task AddCommentAsync(CommentDTO comment);
    }
}
