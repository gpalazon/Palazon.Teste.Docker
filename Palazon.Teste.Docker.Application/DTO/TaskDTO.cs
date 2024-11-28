using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palazon.Teste.Docker.Application.DTO
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } 
        public string Priority { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }

    public sealed class UpdateTaskDTO : TaskDTO
    {
        public int UserId { get; set; }
    }
}
