using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xZoneAPI.badgesLogic
{
    public abstract class AbstractBadge
    {
        public int Id;
        public abstract bool evaluate(int userID);
    }
}
