using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Ranks;
using xZoneAPI.Repositories.AccountRepo;
using static xZoneAPI.Models.Accounts.Account;

namespace xZoneAPI.Logic.RankLogic
{
    public class RankLogic : IRankLogic
    {
        IAccountRepo _accountRepo;
        public RankLogic(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }
        public RankType getNewRank(int userId)
        {
            RankType OldRank = _accountRepo.FindAccountById(userId).Rank;

            return 0;

        }
    }
}
