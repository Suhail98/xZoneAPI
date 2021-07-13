using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.badgesLogic.badges;

namespace xZoneAPI.badgesLogic
{
    public class BadgesSetFactory 
    {
        public List<AbstractBadge> createFullAchievementSet()
        {
            List<AbstractBadge> achievements = new List<AbstractBadge>();
            achievements.Add(new FiveTasksBadge(TaskRepository));
            return achievements;
        }
    }
}
