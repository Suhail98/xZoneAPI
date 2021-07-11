using System.Collections.Generic;

namespace xZoneAPI.badgesLogic
{
    public interface IBadgesSetFactory
    {
        List<IBadge> createFullAchievementSet();
    }
}