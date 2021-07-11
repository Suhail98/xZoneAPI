using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Ranks;
namespace xZoneAPI.Repositories.Ranks
{
    public interface IRankRepo
    {
        bool AddRank(Rank Rank);
        Rank FindRankById(int Id);
        ICollection<Rank> GetAllRanks();
        bool DeleteRank(Rank Rank);
        bool UpdateRank(Rank Rank);
        public bool Save();
        Rank FindRankByName(string name);
    }
}
