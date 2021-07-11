using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Data;
using xZoneAPI.Models.ProjectTaskModel;
using xZoneAPI.Models.TaskModel;

namespace xZoneAPI.Repositories.TaskRepo
{
    public class ProjectTaskRepository : IProjectTaskRepository
    {
        private readonly ApplicationDBContext db;
        public ProjectTaskRepository(ApplicationDBContext _db)
        {
            db = _db;
        }
        public bool AddTask(ProjectTask NewTak)
        {
            db.Add(NewTak);
            return Save();
        }

        public bool DeleteTask(ProjectTask task)
        {
            db.Remove(task);
            return Save();
        }

        public ProjectTask GetTask(int TaskId)
        {
            return db.ProjectTasks.FirstOrDefault(a => a.Id == TaskId);
        }

        public ProjectTask GetTask(string taskName)
        {
            return db.ProjectTasks.FirstOrDefault(a => a.Name == taskName);
        }

        public ICollection<ProjectTask> GetTasks(int sectionID)
        {
            return db.ProjectTasks.OrderBy(a => a.SectionID == sectionID).ToList();
        }

        public bool IsTaskExists(ProjectTask task)
        {
            ProjectTask t = db.ProjectTasks.FirstOrDefault(a => (a.Id == task.Id) && (a.SectionID == task.SectionID));
            return t != null;
        }

        /*public ProjectTask IsTaskExists(ProjectTask task)
        {
            ProjectTask t = db.ProjectTasks.FirstOrDefault(a => (a.Id == task.Id) && (a.UserId == task.UserId) && (a.Name == task.Name));
            return t;
        }*/

        public bool Save()
        {
            return db.SaveChanges() >= 0;
        }

        public bool UpdateTask(ProjectTask task)
        {
            db.Update(task);
            return Save();
        }
    }
}
