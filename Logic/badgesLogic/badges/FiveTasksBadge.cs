using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Repositories.TaskRepo;

namespace xZoneAPI.badgesLogic.badges
{
    public class FiveTasksBadge : AbstractBadge
    {
        ITaskRepository _taskRepo;
        public FiveTasksBadge(ITaskRepository taskRepo)
        {
            _taskRepo = taskRepo;
            Id = 2;
        }
        public override bool evaluate(int userID)
        {
            int numOfFinishedTasks = _taskRepo.GetFinishedTasks(userID);
            return numOfFinishedTasks >= 2 ? true : false; 
        }

       
    }
}
