using Microsoft.AspNetCore.Mvc;
using Palazon.Teste.Docker.Application.DTO;
using Palazon.Teste.Docker.Application.Interfaces;
using Palazon.Teste.Docker.Domain.Models;

namespace Palazon.Teste.Docker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/task/{projectId}
        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetTasksByProjectId(int projectId)
        {
            var tasks = await _taskService.GetTasksByProjectIdAsync(projectId);
            if (tasks == null || tasks.Count == 0)
                return NotFound("Não foram encontradas Tarefas para esse Projeto");

            return Ok(tasks);
        }

        // POST: api/task
        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] TaskDTO task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _taskService.AddTaskAsync(task);
                return CreatedAtAction(nameof(GetTasksByProjectId), new { projectId = task.ProjectId }, task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/task/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskDTO task)
        {
            if (id != task.Id)
                return BadRequest("Task ID inválido.");

            try
            {
                await _taskService.UpdateTaskAsync(task, task.UserId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/task/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                await _taskService.DeleteTaskAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/task
        [HttpPost("comment")]
        public async Task<IActionResult> AddComment([FromBody] CommentDTO comentario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _taskService.AddCommentAsync(comentario);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
