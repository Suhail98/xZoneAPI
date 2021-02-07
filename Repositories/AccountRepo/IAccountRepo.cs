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
        ICollection<Account> GetAllAccounts();
        bool DeleteAccount(Account account);
        bool UpdateAccount(Account account);
        public bool Save();
    }
}
