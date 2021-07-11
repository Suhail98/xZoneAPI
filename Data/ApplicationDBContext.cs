using Microsoft.EntityFrameworkCore;
using System;
using xZoneAPI.Models.Accounts;
using xZoneAPI.Models.TaskModel;
using xZoneAPI.Models;
using xZoneAPI.Models.ProjectModel;
using xZoneAPI.Models.SectionModel;
using xZoneAPI.Models.ProjectTaskModel;
using xZoneAPI.Models.RoadmapModel;
//using System.Data.Entity;

namespace xZoneAPI.Data
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppTask>().ToTable("Tasks");
            modelBuilder.Entity<ProjectTask>().ToTable("ProjectTasks");
         
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AppTask> appTasks { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<ProjectTask> ProjectTasks { get; set; }

        public DbSet<Roadmap> Roadmaps { get; set; }


        //   public DbSet<Skill> skills { get; set; }
    }
}
