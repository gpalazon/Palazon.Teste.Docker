using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palazon.Teste.Docker.Application.DTO
{
    public class UserTaskPerformanceDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int CompletedTasks { get; set; }
        public double AverageCompletedTasks { get; set; }
    }
}
