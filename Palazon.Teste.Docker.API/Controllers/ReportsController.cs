using Microsoft.AspNetCore.Mvc;
using Palazon.Teste.Docker.Application.Interfaces;
using Palazon.Teste.Docker.Application.Services;

namespace Palazon.Teste.Docker.API.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IUserService _userService;
        public ReportsController(IReportService reportService, IUserService userService)
        {
            _reportService = reportService;
            _userService = userService;
        }

        [HttpGet("performance")]
        public async Task<IActionResult> GetPerformanceReport(int userId)
        {
            //if (userRole != "manager")
            //    return Forbid("Access denied. Only managers can access this report.");

            var usuario = await _userService.GetUserByIdAsync(userId);
            if (usuario == null || usuario.Profile.Name.ToUpper() != "GERENTE")
                return NotFound("Usuário não possui acesso a essa funcionalidade.");

            var report = await _reportService.GetTaskCompletionReportAsync();
            return Ok(report);
        }
    }
}
