using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.badgesLogic.badges;
using xZoneAPI.Repositories.AccountRepo;
using xZoneAPI.Repositories.TaskRepo;

namespace xZoneAPI.badgesLogic
{
    public class BadgesSetFactory : IBadgesSetFactory
    {
        ITaskRepository _taskRepo;
        IProjectTaskRepository _projectTaskRepo;
        IAccountZoneTaskRepo _accountZoneTaskRepo;

        public BadgesSetFactory(ITaskRepository appTaskRepo, IAccountZoneTaskRepo accountZoneTaskRepo, IProjectTaskRepository projectTaskRepository)
        {
            _taskRepo = appTaskRepo;
            _accountZoneTaskRepo = accountZoneTaskRepo;
            _projectTaskRepo = projectTaskRepository;
        }

        public List<AbstractBadge> createFullAchievementSet()
        {
            List<AbstractBadge> achievements = new List<AbstractBadge>();
            achievements.Add(new FiveTasksBadge(_taskRepo,_projectTaskRepo,_accountZoneTaskRepo));
            return achievements;
        }



    }
}
