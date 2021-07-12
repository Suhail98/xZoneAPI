using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.badgesLogic;
using xZoneAPI.Logic.RankLogic;
using xZoneAPI.Repositories.Ranks;

namespace xZoneAPI.Logic
{
    public class GamificationLogic
    {
        
        List<AbstractBadge> badges;
        IRankLogic rankLogic;

        public GamificationLogic(IBadgesSetFactory factory, IRankLogic rankLogic)
        {
            badges = factory.createFullAchievementSet();
            this.rankLogic = rankLogic;
        }
        public AchievmentsNotifications checkForNewAchievements(int userID)
        {
            AchievmentsNotifications achievmentsNotifications = new AchievmentsNotifications();
            achievmentsNotifications.badges = getNewBadges(userID);
            achievmentsNotifications.newRank = rankLogic.getNewRank(userID);
            return achievmentsNotifications;
        }
        private List<AbstractBadge> getNewBadges(int userID)
        {
            List<AbstractBadge> badges = new List<AbstractBadge>();
            foreach(AbstractBadge badge in badges)
            {
                if (badge.evaluate(userID))
                    badges.Add(badge);
            }
            return badges;
        }
    }
}
