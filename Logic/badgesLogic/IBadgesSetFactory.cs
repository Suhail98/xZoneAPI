using System.Collections.Generic;

namespace xZoneAPI.badgesLogic
{
    public interface IBadgesSetFactory
    {
        List<AbstractBadge> createFullAchievementSet();
    }
}