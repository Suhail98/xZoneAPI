using Microsoft.EntityFrameworkCore;
using System;
using xZoneAPI.Models.Accounts;
using xZoneAPI.Models.Badges;
using xZoneAPI.Models.Ranks;
using xZoneAPI.Models.Skills;
using xZoneAPI.Models.TaskModel;
using xZoneAPI.Models;
using xZoneAPI.Models.ProjectModel;
using xZoneAPI.Models.SectionModel;
using xZoneAPI.Models.ProjectTaskModel;
using xZoneAPI.Models.RoadmapModel;
using xZoneAPI.Models.Zones;
using xZoneAPI.Models.Posts;
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
            modelBuilder.Entity<AccountSkill>().HasKey(ba => new { ba.AccountID, ba.SkillID });
            modelBuilder.Entity<AccountBadge>().HasKey(ba => new { ba.AccountID, ba.BadgeID });
            modelBuilder.Entity<ZoneMember>().HasKey(ba => new { ba.ZoneId, ba.AccountId });
            modelBuilder.Entity<ZoneSkill>().HasKey(ba => new { ba.ZoneId, ba.SkillId });
            modelBuilder.Entity<AppTask>().ToTable("Tasks");
            modelBuilder.Entity<AccountSkill>().ToTable("AccountSkills");
            modelBuilder.Entity<ProjectTask>().ToTable("ProjectTasks");

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AppTask> appTasks { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<AccountSkill> AccountSkills { get; set; }
        public DbSet<AccountBadge> AccountBadges { get; set; }
      
        public DbSet<Project> Projects { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<ProjectTask> ProjectTasks { get; set; }

        public DbSet<Roadmap> Roadmaps { get; set; }

        public DbSet<Zone> Zones { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<ZoneSkill> ZoneSkills { get; set; }
        public DbSet<ZoneMember> ZoneMembers { get; set; }

    }
}
