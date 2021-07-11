using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xZoneAPI.badgesLogic.badges
{
    public class FiveTasksBadge : AbstractBadge
    {
        public override bool evaluate(int userID)
        {
            return true;
        }

       
    }
}
