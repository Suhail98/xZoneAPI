using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Accounts;
namespace xZoneAPI.Repositories.AccountBadges
{
    public interface IAccountBadgeRepo
    {
        bool AddAccountBadge(AccountBadge AccountBadge);
        ICollection<AccountBadge> GetAllAccountBadges();
        bool DeleteAccountBadge(AccountBadge AccountBadge);
        public AccountBadge GetAccountBadge(int accountId, int BadgeId);
        bool UpdateAccountBadge(AccountBadge AccountBadge);
        public ICollection<AccountBadge> GetAllBadgesForAccount(int Id);
        public bool Save();
    }
}
