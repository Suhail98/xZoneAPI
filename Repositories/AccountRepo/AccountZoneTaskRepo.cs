using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Data;
using xZoneAPI.Models.Accounts;

namespace xZoneAPI.Repositories.AccountRepo
{
    public class AccountZoneTaskRepo : IAccountZoneTaskRepo
    {
        private readonly ApplicationDBContext db;
        public AccountZoneTaskRepo(ApplicationDBContext _db)
        {
            db = _db;
        }
        public AccountZoneTask AddTask(AccountZoneTask NewTak)
        {
            db.AccountZoneTasks.Add(NewTak);
            Save();
            return NewTak;
        }
        public bool DeleteTask(AccountZoneTask task)
        {
            db.AccountZoneTasks.Remove(task);
            return Save();
        }

        public ICollection<AccountZoneTask> GetAllAccountZoneTasks()
        {
            return db.AccountZoneTasks.ToList();
        }
        AccountZoneTask GetAllAccountZoneTasks(int accountID, int zoneTaskID)
        {
            return db.AccountZoneTasks.SingleOrDefault(u => u.AccountID == accountID && u.ZoneTaskID == zoneTaskID);
        }
        public ICollection<AccountZoneTask> GetAllAccountZoneTasksForAccount(int id)
        {
            return db.AccountZoneTasks.Include(u=>u.ZoneTask).Where(u => u.AccountID == id).ToList();
        }

        public bool Save()
        {
            return db.SaveChanges() >= 0;
        }
        public bool UpdateTask(AccountZoneTask task)
        {
            db.Update(task);
            return Save();
        }

        ICollection<AccountZoneTask> IAccountZoneTaskRepo.GetAllAccountZoneTasks(int accountID, int zoneTaskID)
        {
            return db.AccountZoneTasks.Where(u => u.AccountID == accountID && u.ZoneTaskID == zoneTaskID).ToList();
        }
    }
}
