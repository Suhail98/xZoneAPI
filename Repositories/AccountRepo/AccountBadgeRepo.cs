using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Data;
using xZoneAPI.Models.Accounts;
using xZoneAPI.Repositories.AccountBadges;

namespace xZoneAPI.Repositories.AccountRepo
{
    
    public class AccountBadgeRepo : IAccountBadgeRepo
    {
        private readonly ApplicationDBContext _db;
        public AccountBadgeRepo(ApplicationDBContext db)
        {
            _db = db;
        }
        public bool AddAccountBadge(AccountBadge AccountBadge)
        {
            _db.AccountBadges.Add(AccountBadge);
            return Save();
        }
        public bool DeleteAccountBadge(AccountBadge AccountBadge)
        {
            _db.AccountBadges.Remove(AccountBadge);
            return Save();
        }
        public ICollection<AccountBadge> GetAllAccountBadges()
        {
            return _db.AccountBadges.ToList();
        }
        public ICollection<AccountBadge> GetAllBadgesForAccount(int Id)
        {
            return _db.AccountBadges.Where(a => a.AccountID == Id).ToList();
        }
        public bool UpdateAccountBadge(AccountBadge AccountBadge)
        {
            _db.Update(AccountBadge);
            return Save();
        }
        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }

        public AccountBadge GetAccountBadge(int accountId, int BadgeId)
        {
            return _db.AccountBadges.SingleOrDefault(a => a.AccountID == accountId && a.BadgeID == BadgeId);

        }
    }

    
}
