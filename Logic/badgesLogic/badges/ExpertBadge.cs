using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.badgesLogic;
using xZoneAPI.Repositories.AccountRepo;
using xZoneAPI.Repositories.TaskRepo;

namespace xZoneAPI.Logic.badgesLogic.badges
{
    public class ExpertBadge : AbstractBadge
    {
        ITaskRepository _taskRepo;
        IProjectTaskRepository _projectTaskRepo;
        IAccountZoneTaskRepo _accountZoneTaskRepo;
        public ExpertBadge(ITaskRepository taskRepo, IProjectTaskRepository projectTaskRepository, IAccountZoneTaskRepo accountZoneTaskRepo)
        {
            _taskRepo = taskRepo;
            _accountZoneTaskRepo = accountZoneTaskRepo;
            _projectTaskRepo = projectTaskRepository;
            Id = 5;
        }
        public override bool evaluate(int userID)
        {
            int numOfFinishedTasks = _taskRepo.GetFinishedTasks(userID) + _projectTaskRepo.GetFinishedTasks(userID) + _accountZoneTaskRepo.GetFinishedTasks(userID);
            return numOfFinishedTasks >= 100 ? true : false;
        }
    }
}
