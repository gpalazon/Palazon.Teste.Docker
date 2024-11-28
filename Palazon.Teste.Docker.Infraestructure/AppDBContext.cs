using Microsoft.EntityFrameworkCore;
using Palazon.Teste.Docker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Palazon.Teste.Docker.Infraestructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<TaskHistory> TaskHistories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasKey(p => p.Id);
            modelBuilder.Entity<Tasks>().HasKey(t => t.Id);
            modelBuilder.Entity<TaskHistory>().HasKey(th => th.Id);
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Profile>().HasKey(p => p.Id);

            //User
            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.UserId);
            
            modelBuilder.Entity<TaskHistory>()
            .HasOne(h => h.ModifiedBy)
            .WithMany(u => u.TaskHistories)
            .HasForeignKey(h => h.ModifiedById);

            //Project
            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId);

            //Tasks           
            modelBuilder.Entity<TaskHistory>()
            .HasOne(h => h.Task)
            .WithMany(t => t.Histories)
            .HasForeignKey(h => h.TaskId);

            //Profile
            modelBuilder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithMany(p => p.Users)
                .HasForeignKey(u => u.ProfileId);

        }
    }
}
