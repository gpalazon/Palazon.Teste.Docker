using Palazon.Teste.Docker.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palazon.Teste.Docker.Application.Interfaces
{
  
    public interface ITaskHistoryService
    {
        Task<List<TaskHistoryDTO>> GetTaskHistoriesAsync(int taskId);
     
    }
}
