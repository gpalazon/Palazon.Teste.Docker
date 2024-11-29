using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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

        // DbSets para representar as tabelas
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<TaskHistory> TaskHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            // Configurações específicas para cada entidade
            ConfigureUser(modelBuilder);
            ConfigureProfile(modelBuilder);
            ConfigureProject(modelBuilder);
            ConfigureTasks(modelBuilder);
            ConfigureTaskHistory(modelBuilder);

          
        }


        private void ConfigureUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(u => u.Id);

                entity.Property(u => u.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.Mail)
                    .IsRequired();

                entity.HasOne(u => u.Profile)
                    .WithMany(p => p.Users)
                    .HasForeignKey(u => u.ProfileId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(u => u.Projects)
                    .WithOne(p => p.User)
                    .HasForeignKey(p => p.UserId);

                entity.HasMany(u => u.Tasks)
                    .WithOne(t => t.User)
                    .HasForeignKey(t => t.UserId);

                entity.HasMany(u => u.TaskHistories)
                    .WithOne(th => th.ModifiedBy)
                    .HasForeignKey(th => th.ModifiedById)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureProfile(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("Profiles");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }

        private void ConfigureProject(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Projects");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(p => p.User)
                    .WithMany(u => u.Projects)
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigureTasks(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.ToTable("Tasks");

                entity.HasKey(t => t.Id);

                entity.Property(t => t.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(t => t.Description)
                    .HasMaxLength(500);

                entity.Property(t => t.Status)
                    .IsRequired();

                entity.Property(t => t.Priority)
                    .IsRequired();

                entity.HasOne(t => t.Project)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(t => t.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(t => t.User)
                    .WithMany(u => u.Tasks)
                    .HasForeignKey(t => t.UserId);

                entity.HasMany(t => t.Histories)
                    .WithOne(h => h.Task)
                    .HasForeignKey(h => h.TaskId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigureTaskHistory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskHistory>(entity =>
            {
                entity.ToTable("TaskHistories");

                entity.HasKey(th => th.Id);

                entity.Property(th => th.PropertyName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(th => th.NewValue)
                    .HasMaxLength(500);

                entity.Property(th => th.ModifiedAt)
                    .IsRequired();

                entity.HasOne(th => th.Task)
                    .WithMany(t => t.Histories)
                    .HasForeignKey(th => th.TaskId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(th => th.ModifiedBy)
                    .WithMany(u => u.TaskHistories)
                    .HasForeignKey(th => th.ModifiedById)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }


    }
}
