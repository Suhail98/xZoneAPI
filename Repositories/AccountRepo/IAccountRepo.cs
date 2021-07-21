using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Accounts;

namespace xZoneAPI.Repositories.AccountRepo
{
    public interface IAccountRepo
    {
        Account register(Account account);
        Account FindAccountByEmail(string Email);
        Account FindAccountById(int Id);
        Account AuthenticateUser(string email, string password);
        Account GetAccountWithItsBadges(int accountId);
        ICollection<Account> GetAllAccounts();
        Account getProfile(int AccountId);
        bool DeleteAccount(Account account);
        bool UpdateAccount(Account account);
        public bool Save();
        public List<Account> FindAccountByName(string name);
    }
}
