using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Data;
using xZoneAPI.Models.Ranks;

namespace xZoneAPI.Repositories.Ranks
{
    public class RankRepo : IRankRepo
    {
        private readonly ApplicationDBContext _db;
        public RankRepo(ApplicationDBContext db)
        {
            _db = db;
        }
        public bool AddRank(Rank Rank)
        {
            _db.Ranks.Add(Rank);
            return Save();
        }

        public bool DeleteRank(Rank Rank)
        {
            _db.Ranks.Remove(Rank);
            return Save();
        }

        public Rank FindRankById(int Id)
        {
            return _db.Ranks.FirstOrDefault(a => a.Id == Id);
        }

        public ICollection<Rank> GetAllRanks()
        {
            return _db.Ranks.ToList();
        }
        public bool UpdateRank(Rank Rank)
        {
            _db.Update(Rank);
            return Save();
        }
        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }
        public Rank FindRankByName(string name)
        {
            return _db.Ranks.FirstOrDefault(a => a.Name == name);
        }

    }
}
