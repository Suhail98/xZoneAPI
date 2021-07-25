using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        public ProjectTask AddTask(ProjectTask NewTak)
        {
            db.Add(NewTak);
            Save();
            return NewTak;
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
            return db.ProjectTasks.Where(a => a.SectionID == sectionID).ToList();
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

        public ICollection<DateTime> GetListOfActiveDays(int userId)
        {
            ICollection<DateTime> activeDayes = db.ProjectTasks
                .Include(u => u.ParentSection)
                .ThenInclude(u => u.ParentProject)
                .Where(u => u.CompleteDate.HasValue
                && u.ParentSection.ParentProject.OwnerID == userId).Select(u => u.CompleteDate.Value.Date).Distinct().ToList();
            return activeDayes;
        }

        public int GetFinishedTasks(int userId)
        {
            int count = db.ProjectTasks
                .Include(u=>u.ParentSection)
                .ThenInclude(u=>u.ParentProject)
                .Count(u => u.CompleteDate != null
                && u.ParentSection.ParentProject.OwnerID == userId);
            return count;
        }

    }
}
