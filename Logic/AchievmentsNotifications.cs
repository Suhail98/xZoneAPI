using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Badges;
using xZoneAPI.Models.Ranks;

namespace xZoneAPI.Logic
{
    public class AchievmentsNotifications
    {
        public ICollection<Badge> badges;
        public Rank newRank;
    }
}
