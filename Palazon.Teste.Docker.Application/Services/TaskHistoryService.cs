using Palazon.Teste.Docker.Application.DTO;
using Palazon.Teste.Docker.Application.Interfaces;
using Palazon.Teste.Docker.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palazon.Teste.Docker.Application.Services
{
    public class TaskHistoryService : ITaskHistoryService
    {
       
        private readonly ITaskHistoryRepository _historyRepository;

        public TaskHistoryService(ITaskRepository taskRepository, ITaskHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }

        public async Task<List<TaskHistoryDTO>> GetTaskHistoriesAsync(int taskId)
        {
            var histories = await _historyRepository.GetTaskHistoriesAsync(taskId);

            // Mapeando para o DTO
            return histories.Select(p => new TaskHistoryDTO
            {
                Id = p.Id,
                ModifiedById = p.ModifiedById,  
                ModifiedAt = p.ModifiedAt,  
                ModifiedByName = p.ModifiedBy.Name,
                NewValue = p.NewValue,  
                PropertyName = p.PropertyName,  
                TaskId = p.TaskId   
            }).ToList();

        }
    }
}
