using Microsoft.EntityFrameworkCore;
using System;
using xZoneAPI.Models.Accounts;
using xZoneAPI.Models.Badges;
using xZoneAPI.Models.Ranks;
using xZoneAPI.Models.Skills;
using xZoneAPI.Models.TaskModel;
//using System.Data.Entity;

namespace xZoneAPI.Data
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AppTask> appTasks { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<AccountSkill> AccountSkills { get; set; }
        public DbSet<AccountBadge> AccountBadges { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountSkill>().HasKey(ba => new { ba.AccountID, ba.SkillID });
            modelBuilder.Entity<AccountBadge>().HasKey(ba => new { ba.AccountID, ba.BadgeID });

        }
    }
}
