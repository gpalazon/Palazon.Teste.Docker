using Palazon.Teste.Docker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palazon.Teste.Docker.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);

        Task<List<User>> GetAllUsersAsync();
    }
}
