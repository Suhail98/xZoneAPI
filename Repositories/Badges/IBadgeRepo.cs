using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Badges;
namespace xZoneAPI.Repositories.Badges
{
    public interface IBadgeRepo
    {
        Badge AddBadge(Badge Badge);
        Badge FindBadgeById(int Id);
        ICollection<Badge> GetAllBadges();
        bool DeleteBadge(Badge Badge);
        bool UpdateBadge(Badge Badge);
        public bool Save();
        Badge FindBadgeByName(string name);
    }
}
