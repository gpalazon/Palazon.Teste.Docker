using Palazon.Teste.Docker.Application.Interfaces;
using Palazon.Teste.Docker.Domain.Interfaces;
using Palazon.Teste.Docker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palazon.Teste.Docker.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;

        public UserService(IUserRepository UserRepository) => _UserRepository = UserRepository;

        public async Task<User> GetUserByIdAsync(int id) => await _UserRepository.GetUserByIdAsync(id);

        public async Task<List<User>> GetAllUsersAsync() => await _UserRepository.GetAllUsersAsync();
    }
}
