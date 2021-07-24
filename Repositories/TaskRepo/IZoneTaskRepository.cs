using System.Collections.Generic;
using xZoneAPI.Models.TaskModel;

namespace xZoneAPI.Repositories.TaskRepo
{
    public interface IZoneTaskRepository
    {
        ZoneTask AddTask(ZoneTask NewTak);
        bool DeleteTask(ZoneTask task);
        ZoneTask GetTask(int TaskId);
        ZoneTask GetTask(string taskName);
        public ICollection<ZoneTask> GetTasks(int ZoneId);
        bool Save();
        bool UpdateTask(ZoneTask task);
        public int GetFinishedTasks(int userId);
    }
}