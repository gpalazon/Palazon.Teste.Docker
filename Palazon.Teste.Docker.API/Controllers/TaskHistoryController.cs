using Microsoft.AspNetCore.Mvc;
using Palazon.Teste.Docker.Application.Interfaces;
using Palazon.Teste.Docker.Domain.Models;

namespace Palazon.Teste.Docker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskHistoryController : ControllerBase
    {
        private readonly ITaskHistoryService _historyService;
        private readonly ILogger<ProjectController> _logger;

        public TaskHistoryController(ITaskHistoryService historyService,
           ILogger<ProjectController> logger)
        {
            _historyService = historyService;
            _logger = logger;
        }

        // GET: api/project
        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetAll(int taskId)
        {
            var histories = await _historyService.GetTaskHistoriesAsync(taskId);
            if (histories == null || histories.Count == 0)
                return NotFound("Não foi encontrado nenhum histórico para essa Tarefa");

            return Ok(histories);
        }

    }
}
