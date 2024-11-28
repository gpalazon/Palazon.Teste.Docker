using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palazon.Teste.Docker.Domain.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Mail { get; set; }

        public int ProfileId { get; set; }

        public Profile Profile { get; set; }

        public List<Project> Projects { get; set; } = new();

        public List<Tasks> Tasks { get; set; } = new();

        public List<TaskHistory> TaskHistories { get; set; } = new ();
    }
}
