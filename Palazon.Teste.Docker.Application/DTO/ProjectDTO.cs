using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palazon.Teste.Docker.Application.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do Projeto é obrigatório")]
        [StringLength(200, ErrorMessage = "O nome não pode exceder 200 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O Usuário é obrigatório")]
        public int UserId { get; set; }

        public int TaskCount { get; set; }
    }
}
