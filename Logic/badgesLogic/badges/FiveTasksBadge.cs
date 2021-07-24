using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Repositories.AccountRepo;
using xZoneAPI.Repositories.TaskRepo;

namespace xZoneAPI.badgesLogic.badges
{
    public class FiveTasksBadge : AbstractBadge
    {
        ITaskRepository _taskRepo;
        IProjectTaskRepository _projectTaskRepo;
        IAccountZoneTaskRepo _accountZoneTaskRepo;
        public FiveTasksBadge(ITaskRepository taskRepo, IProjectTaskRepository projectTaskRepository, IAccountZoneTaskRepo accountZoneTaskRepo)
        {
            _taskRepo = taskRepo;
            _accountZoneTaskRepo = accountZoneTaskRepo;
            _projectTaskRepo = projectTaskRepository;
            Id = 2;
        }
        public override bool evaluate(int userID)
        {
            int numOfFinishedTasks = _taskRepo.GetFinishedTasks(userID) + _projectTaskRepo.GetFinishedTasks(userID) + _accountZoneTaskRepo.GetFinishedTasks(userID);
            return numOfFinishedTasks >= 2 ? true : false; 
        }

       
    }
}
