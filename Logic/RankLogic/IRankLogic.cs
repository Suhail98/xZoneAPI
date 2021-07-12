using xZoneAPI.Models.Ranks;
using static xZoneAPI.Models.Accounts.Account;

namespace xZoneAPI.Logic.RankLogic
{
    public interface IRankLogic
    {
        RankType getNewRank(int userId);
    }
}