using Microsoft.AspNetCore.Mvc;
using Palazon.Teste.Docker.Application.DTO;
using Palazon.Teste.Docker.Application.Interfaces;

namespace Palazon.Teste.Docker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(IProjectService projectService,
            ILogger<ProjectController> logger)
        {
            _projectService = projectService;
            _logger = logger;
        }

        // GET: api/project
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllProjects(int userId)
        {
            var projects = await _projectService.GetAllProjectsAsync(userId);
            if (projects == null || projects.Count == 0)
                return NotFound("Não foi encontrado nenhum Projeto");

            return Ok(projects);
        }

        // GET: api/project/{id}
        [HttpGet("{id}/view")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
                return NotFound($"Projeto com o ID {id} não encontrado.");

            return Ok(project);
        }

        // POST: api/project
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _projectService.CreateProjectAsync(project);
            return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, project);
        }

        // DELETE: api/project/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                await _projectService.DeleteProjectAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
