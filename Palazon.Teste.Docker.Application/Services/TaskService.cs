using Palazon.Teste.Docker.Application.DTO;
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
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskHistoryRepository _historyRepository;

        public TaskService(ITaskRepository taskRepository, ITaskHistoryRepository historyRepository)
        {
            _taskRepository = taskRepository;
            _historyRepository = historyRepository;
        }

        /// <summary>
        /// Retorna todas as tarefas de um projeto
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<List<TaskDTO>> GetTasksByProjectIdAsync(int projectId)
        {
            var tarefas = await _taskRepository.GetTasksByProjectIdAsync(projectId);

            // Mapeando para o DTO
            return tarefas.Select(p => new TaskDTO
            {
                Id = p.Id,
               Description = p.Description, 
               DueDate = p.DueDate, 
               Priority = p.Priority,
               ProjectId = p.ProjectId,
               Status = p.Status,
               Title = p.Title,
            }).ToList();

        }

        /// <summary>
        /// Cria nova tarefa para um projeto e usuário
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task AddTaskAsync(TaskDTO task)
        {
            var existingTasks = await _taskRepository.GetTasksByProjectIdAsync(task.ProjectId);
            if (existingTasks.Count >= 20)
                throw new Exception("Limite de tarefas atingido para esse projeto");

            if (task.Priority is not ("Baixa" or "Média" or "Alta"))
                throw new Exception("Prioridade inválida.");


            var newTask = new Tasks
            {
                Id = task.Id,
                Description = task.Description,
                Title = task.Title,
                Status = task.Status,
                ProjectId = task.ProjectId,
                Priority = task.Priority,
                DueDate = task.DueDate,
                UserId = task.UserId
            };
            await _taskRepository.AddTaskAsync(newTask);
            task.Id = newTask.Id;
        }

        /// <summary>
        /// Atualiza os detalhes de uma tarefa existente e registra histórico de alterações.
        /// </summary>
        public async Task UpdateTaskAsync(TaskDTO task, int userId)
        {
            var _task = await _taskRepository.GetTaskByIdAsync(task.Id);

            // Registrar histórico de alterações
            await _historyRepository.AddTaskHistoryAsync(task.Id, "Title", _task.Title, task.Title, userId);
            await _historyRepository.AddTaskHistoryAsync(task.Id, "Description", _task.Description, task.Description, userId);
            await _historyRepository.AddTaskHistoryAsync(task.Id, "Status", _task.Status, task.Status, userId);
            await _historyRepository.AddTaskHistoryAsync(task.Id, "DueDate", _task.DueDate.ToString(), task.DueDate.ToString(), userId);


            await _taskRepository.UpdateTaskAsync(new Tasks
            {
                Id = task.Id,
                Description = task.Description,
                Title = task.Title, 
                Status = task.Status,   
                ProjectId= task.ProjectId,  
                Priority = task.Priority,   
                DueDate= task.DueDate,
                UserId = task.UserId
            });
        }

        /// <summary>
        /// Adiciona um comentário a uma tarefa e registra no histórico.
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task AddCommentAsync(CommentDTO comment)
        {
            var _task = await _taskRepository.GetTaskByIdAsync(comment.TaskId);
            if (_task == null)
                throw new Exception("Tarefa não encontrada.");

            // Registrar histórico de alterações
            await _historyRepository.AddTaskHistoryAsync(comment.TaskId, "Comment", string.Empty, comment.Comment, comment.UserId);
        }

        /// <summary>
        /// Exclui uma tarefa existente.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteTaskAsync(int id)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);
            if (task == null)
                throw new Exception("Tarefa não encontrada.");

            await _taskRepository.DeleteTaskAsync(task);
        }
    }
}
