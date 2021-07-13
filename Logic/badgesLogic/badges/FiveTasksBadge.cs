using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Repositories.TaskRepo;

namespace xZoneAPI.badgesLogic.badges
{
    public class FiveTasksBadge : AbstractBadge
    {
        TaskRepository _taskRepo;
        FiveTasksBadge(ITaskRepository taskRepo)
        {
            _taskRepo = taskRepo;
            Id = 1;
        }
        public override bool evaluate(int userID)
        {
            int numOfFinishedTasks = _taskRepo.GetFinishedTasks(userID);
            return numOfFinishedTasks >= 5 ? true : false; 
        }

       
    }
}
