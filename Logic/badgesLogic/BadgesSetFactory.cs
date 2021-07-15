using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.badgesLogic.badges;
using xZoneAPI.Repositories.TaskRepo;

namespace xZoneAPI.badgesLogic
{
    public class BadgesSetFactory : IBadgesSetFactory
    {
        ITaskRepository _appTaskRepo;

        public BadgesSetFactory(ITaskRepository appTaskRepo)
        {
            _appTaskRepo = appTaskRepo;
        }

        public List<AbstractBadge> createFullAchievementSet()
        {
            List<AbstractBadge> achievements = new List<AbstractBadge>();
            achievements.Add(new FiveTasksBadge(_appTaskRepo));
            return achievements;
        }
    }
}
