using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Data;
using xZoneAPI.Models.TaskModel;

namespace xZoneAPI.Repositories.TaskRepo
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDBContext db;
        public TaskRepository(ApplicationDBContext _db)
        {
            db = _db;
        }
        
        public AppTask AddTask(AppTask NewTak)
        {
            db.appTasks.Add(NewTak);
            Save();
            return NewTak;
        }

        public bool DeleteTask(AppTask task)
        {
            db.appTasks.Remove(task);
            return Save();
        }

        public int GetFinishedTasks(int userId)
        {
            int count = db.appTasks.Count(u => u.CompleteDate != null && u.UserId == userId);
            return count;
        }
        public int GetActiveDays(int userId)
        {
            int count = db.appTasks.Where(u=>u.UserId == userId ).Select(u => u.CompleteDate.Value.Day ).Distinct().Count();
            return count;
        }

        public ICollection<DateTime> GetListOfActiveDays(int userId)
        {
            
            ICollection<DateTime> activeDayes = db.appTasks.Where(u => u.CompleteDate.HasValue && userId == u.UserId ).Select(u => u.CompleteDate.Value.Date).Distinct().ToList();
            return activeDayes;
        }


        public AppTask GetTask(int TaskId)
        {
            return db.appTasks.FirstOrDefault(a => a.Id == TaskId);
        }

        public AppTask GetTask(string taskName)
        {
            return db.appTasks.FirstOrDefault(a => a.Name == taskName);
        }

        public ICollection<AppTask> GetTasks(int userId)
        {
            return db.appTasks.OrderBy(a => a.UserId == userId).ToList();
        }

        public bool IsTaskExists(AppTask task)
        {
            AppTask t = db.appTasks.FirstOrDefault( a => (a.Id == task.Id) && ( a.UserId == task.UserId) );
            return  t != null;
        }

        public bool Save()
        {
            return db.SaveChanges() >= 0;
        }

        public bool UpdateTask(AppTask task)
        {
            db.Update(task);
            return Save();
        }
    }
}
