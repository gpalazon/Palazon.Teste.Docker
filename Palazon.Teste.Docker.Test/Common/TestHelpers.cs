using Microsoft.EntityFrameworkCore;
using Palazon.Teste.Docker.Domain.Models;
using Palazon.Teste.Docker.Infraestructure;
using System;
using System.Collections.Generic;

namespace Palazon.Teste.Docker.Test.Common
{
    public static class TestHelpers
    {
        /// <summary>
        /// Cria uma instância de AppDbContext configurada com um banco de dados em memória.
        /// </summary>
        public static AppDbContext CreateInMemoryDbContext(string databaseName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            return new AppDbContext(options);
        }

        /// <summary>
        /// Cria uma lista de tarefas para uso nos testes.
        /// </summary>
        public static List<Tasks> GetSampleTasks(int projectId = 1)
        {
            return new List<Tasks>
            {
                new Tasks
                {
                    Id = 1,
                    Title = "Task 1",
                    Description = "Description 1",
                    Priority = "Média",
                    Status = "Pending",
                    ProjectId = projectId,
                    DueDate = DateTime.UtcNow.AddDays(1)
                },
                new Tasks
                {
                    Id = 2,
                    Title = "Task 2",
                    Description = "Description 2",
                    Priority = "Alta",
                    Status = "InProgress",
                    ProjectId = projectId,
                    DueDate = DateTime.UtcNow.AddDays(2)
                }
            };
        }

        /// <summary>
        /// Cria uma lista de projetos para uso nos testes.
        /// </summary>
        public static List<Project> GetSampleProjects()
        {
            return new List<Project>
            {
                new Project
                {
                    Id = 1,
                    Name = "Project 1",
                    UserId = 1,
                    Tasks = GetSampleTasks()
                },
                new Project
                {
                    Id = 2,
                    Name = "Project 2",
                    UserId = 2,
                    Tasks = new List<Tasks>()
                }
            };
        }

        /// <summary>
        /// Cria uma lista de históricos de tarefas para uso nos testes.
        /// </summary>
        public static List<TaskHistory> GetSampleTaskHistories(int taskId = 1)
        {
            return new List<TaskHistory>
            {
                new TaskHistory
                {
                    Id = 1,
                    TaskId = taskId,
                    PropertyName = "Title",
                    NewValue = "New Title",
                    ModifiedById = 1
                },
                new TaskHistory
                {
                    Id = 2,
                    TaskId = taskId,
                    PropertyName = "Description",
                    NewValue = "New Description",
                    ModifiedById = 1
                }
            };
        }
    }
}
