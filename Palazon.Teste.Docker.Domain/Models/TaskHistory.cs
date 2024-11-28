using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palazon.Teste.Docker.Domain.Models
{
    public class TaskHistory
    {
        public int Id { get; set; }
        public int TaskId { get; set; }

        public Tasks Task { get; set; }
        public string PropertyName { get; set; }
        public string NewValue { get; set; }    
        public DateTime ModifiedAt { get; set; } 
        public int ModifiedById { get; set; } // FK para o usuário que realizou a alteração
        public User ModifiedBy { get; set; }  // Navegação para o usuário

    }
}
