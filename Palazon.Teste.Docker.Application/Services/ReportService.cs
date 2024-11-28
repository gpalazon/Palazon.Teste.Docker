using Palazon.Teste.Docker.Application.DTO;
using Palazon.Teste.Docker.Application.Interfaces;
using Palazon.Teste.Docker.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palazon.Teste.Docker.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IUserRepository _userRepository;

        public ReportService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserTaskPerformanceDto>> GetTaskCompletionReportAsync()
        {
            var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);
            var users = await _userRepository.GetUsersWithCompletedTasksAsync(thirtyDaysAgo);

            return users.Select(user => new UserTaskPerformanceDto
            {
                UserId = user.Id,
                UserName = user.Name,
                CompletedTasks = user.Tasks.Count(t => t.Status == "Completed" && t.DueDate >= thirtyDaysAgo),
                AverageCompletedTasks = user.Tasks
                    .Where(t => t.Status == "Completed" && t.DueDate >= thirtyDaysAgo)
                    .GroupBy(t => t.Project.UserId)
                    .Select(g => g.Count())
                    .DefaultIfEmpty(0)
                    .Average()
            }).ToList();
        }
    }
}
