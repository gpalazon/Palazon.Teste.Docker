using Palazon.Teste.Docker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palazon.Teste.Docker.Domain.Interfaces
{
    public interface ITaskHistoryRepository
    {
        Task<List<TaskHistory>> GetTaskHistoriesAsync(int taskId);

        Task AddTaskHistoryAsync(int taskId, string propertyName, string oldValue, string newValue, int modifiedBy);


    }
}
