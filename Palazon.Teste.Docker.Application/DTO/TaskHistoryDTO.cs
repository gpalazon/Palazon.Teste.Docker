using Palazon.Teste.Docker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palazon.Teste.Docker.Application.DTO
{
    public  class TaskHistoryDTO
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string PropertyName { get; set; }
        public string NewValue { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int ModifiedById { get; set; } 
        public string ModifiedByName { get; set; } 
    }
}
