using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palazon.Teste.Docker.Domain.Models
{
    public class Profile
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<User> Users { get; set; } = new();
    }
}
