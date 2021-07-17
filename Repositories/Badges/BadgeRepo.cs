using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Data;
using xZoneAPI.Models.Badges;

namespace xZoneAPI.Repositories.Badges
{
    public class BadgeRepo : IBadgeRepo
    {
        private readonly ApplicationDBContext _db;
        public BadgeRepo(ApplicationDBContext db)
        {
            _db = db;
        }
        public Badge AddBadge(Badge Badge)
        {
            _db.Badges.Add(Badge);
            Save();
            return Badge;
        }

        public bool DeleteBadge(Badge Badge)
        {
            _db.Badges.Remove(Badge);
            return Save();
        }

        public Badge FindBadgeById(int Id)
        {
            return _db.Badges.FirstOrDefault(a => a.Id == Id);
        }

        public ICollection<Badge> GetAllBadges()
        {
            return _db.Badges.ToList();
        }
        public bool UpdateBadge(Badge Badge)
        {
            _db.Update(Badge);
            return Save();
        }
        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }
        public Badge FindBadgeByName(string name)
        {
            return _db.Badges.FirstOrDefault(a => a.Name == name);
        }

    }
}
