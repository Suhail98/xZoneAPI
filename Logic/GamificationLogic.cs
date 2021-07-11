using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.badgesLogic;
using xZoneAPI.Repositories.Ranks;

namespace xZoneAPI.Logic
{
    public class GamificationLogic
    {
        List<IBadge> badges;
        public GamificationLogic(IBadgesSetFactory factory)
        {
            badges = factory.createFullAchievementSet();
        }
        public AchievmentsNotifications checkForNewAchievements(int userID)
        {
            
            return null;
        }
        private List<IBadge> getNewBadges(int userID)
        {
            List<IBadge> badges = new List<IBadge>();
            foreach(IBadge badge in badges)
            {
                if (badge.evaluate(userID)
                    badges.Add(badge);
            }
            return badges;
        }
    }
}
