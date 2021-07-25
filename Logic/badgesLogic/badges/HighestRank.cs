using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.badgesLogic;
using xZoneAPI.Repositories.AccountRepo;
using static xZoneAPI.Models.Accounts.Account;

namespace xZoneAPI.Logic.badgesLogic.badges
{
    public class HighestRank : AbstractBadge
    {
        private IAccountRepo accountRepo;
        public HighestRank(IAccountRepo accountRepo)
        {
            Id = 2;
            this.accountRepo = accountRepo;
        }
        public override bool evaluate(int userID)
        {
            return accountRepo.FindAccountById(userID).Rank == RankType.Plat ? true : false;
        }

    }
}
