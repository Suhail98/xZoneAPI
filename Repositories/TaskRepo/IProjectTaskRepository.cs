using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.ProjectTaskModel;

namespace xZoneAPI.Repositories.TaskRepo
{
    public interface IProjectTaskRepository
    {
        ICollection<ProjectTask> GetTasks(int userId);
        ProjectTask GetTask(int TaskId);
        ProjectTask GetTask(string TaskName);
        //public ProjectTask AddTask(int UserId, TaskDto _TaskDto);
        public ProjectTask AddTask(ProjectTask task); // user add
        public bool DeleteTask(ProjectTask task);
        public bool UpdateTask(ProjectTask task);
        bool IsTaskExists(ProjectTask task);
        public ICollection<DateTime> GetListOfActiveDays(int userId);
        public int GetFinishedTasks(int userId);
    }
}
