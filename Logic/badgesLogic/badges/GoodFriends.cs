using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.badgesLogic;
using xZoneAPI.Repositories.AccountRepo;

namespace xZoneAPI.Logic.badgesLogic.badges
{
    public class GoodFriends : AbstractBadge
    {
        IFriendRepository friendRepo;
        public GoodFriends(IFriendRepository friendRepo)
        {
            Id = 4;
            this.friendRepo = friendRepo;
        }
        public override bool evaluate(int userID)
        {
            return friendRepo.GetAllFriendsForAccount(userID).Count() >= 2 ? true : false;
        }
    }
}
