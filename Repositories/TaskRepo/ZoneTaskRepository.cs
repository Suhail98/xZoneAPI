using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Data;
using xZoneAPI.Models.TaskModel;

namespace xZoneAPI.Repositories.TaskRepo
{
    public class AccountZoneTaskRepository : IZoneTaskRepository
    {
        private readonly ApplicationDBContext db;
        public AccountZoneTaskRepository(ApplicationDBContext _db)
        {
            db = _db;
        }

        public ZoneTask AddTask(ZoneTask NewTak)
        {
            db.ZoneTasks.Add(NewTak);
            Save();
            return NewTak;
        }
        public bool DeleteTask(ZoneTask task)
        {
            db.ZoneTasks.Remove(task);
            return Save();
        }

        public ZoneTask GetTask(int TaskId)
        {
            return db.ZoneTasks.FirstOrDefault(a => a.Id == TaskId);
        }
        public ICollection<ZoneTask> GetTasks(int ZoneId)
        {
            return db.ZoneTasks.Where(a => a.ZoneId == ZoneId).ToList();
        }
        public ZoneTask GetTask(string taskName)
        {
            return db.ZoneTasks.FirstOrDefault(a => a.Name == taskName);
        }

        public bool Save()
        {
            return db.SaveChanges() >= 0;
        }
        public bool UpdateTask(ZoneTask task)
        {
            db.Update(task);
            return Save();
        }

        public int GetFinishedTasks(int userId)
        {
            int count = db.AccountZoneTasks.Count(u => u.CompleteDate != null);
            return count;
        }


    }
}