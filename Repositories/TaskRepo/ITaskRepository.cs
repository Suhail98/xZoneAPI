using System;
using System.Collections.Generic;
using System.Linq;
using xZoneAPI.Models.TaskModel;


namespace xZoneAPI.Repositories.TaskRepo
{
    public interface ITaskRepository
    {
        ICollection<AppTask> GetTasks(int userId);
        AppTask GetTask(int TaskId);
        AppTask GetTask(string TaskName);
        //public AppTask AddTask(int UserId, TaskDto _TaskDto);
        public bool AddTask(AppTask task); // user add
        public bool DeleteTask(AppTask task);
        public bool UpdateTask(AppTask task);
        bool IsTaskExists(AppTask task);
        // share
        bool Save();
    }
}
