using ETT.Storage.Entities;
using ETT.Storage.Entities.Projects;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
//using Microsoft.Extensions.Configuration.Json;

namespace ETT.Storage.Context
{
    public class ApplicationContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }

        //public DbSet<Timecard> Timecards { get; set; }
        public DbSet<Record> Records { get; set; }

        public IConfiguration AppConfiguration { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            // создаем конфигурацию
            AppConfiguration = builder.Build();
            optionsBuilder.UseSqlServer(AppConfiguration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectTask>()
            .HasOne(p => p.Project)
            .WithMany(t => t.Tasks)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectUser>()
            .HasOne(p => p.Project)
            .WithMany(u => u.Users)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Record>()
            .HasOne(t => t.Task)
            .WithMany(r => r.Records)
            .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
