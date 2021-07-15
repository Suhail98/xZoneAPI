using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.badgesLogic;
using xZoneAPI.Models.Badges;
using xZoneAPI.Models.Ranks;
using static xZoneAPI.Models.Accounts.Account;

namespace xZoneAPI.Logic
{
    public class AchievmentsNotifications
    {
        public List<int> badges;
        public RankType? newRank;
    }
}
