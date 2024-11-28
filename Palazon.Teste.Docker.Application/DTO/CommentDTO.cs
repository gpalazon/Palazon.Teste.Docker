using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palazon.Teste.Docker.Application.DTO
{
    public class CommentDTO
    {
        public int TaskId { get; set; }

        public int UserId { get; set; }

        public string Comment { get; set; }
    }
}
