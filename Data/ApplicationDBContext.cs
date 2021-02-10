using Microsoft.EntityFrameworkCore;
using Models.Skill;
using System;
using xZoneAPI.Models.Accounts;
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
     //   public DbSet<Skill> skills { get; set; }
    }
}
