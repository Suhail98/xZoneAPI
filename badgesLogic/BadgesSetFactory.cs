using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.badgesLogic.badges;

namespace xZoneAPI.badgesLogic
{
    public class BadgesSetFactory : IBadgesSetFactory
    {
        public List<IBadge> createFullAchievementSet()
        {
            List<IBadge> achievements = new List<IBadge>();
            achievements.Add(new FiveTasksBadge());
            return achievements;
        }
    }
}
